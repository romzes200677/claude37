using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Courses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий рекомендаций
    /// </summary>
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly CoursesDbContext _context;
        private readonly ILogger<RecommendationRepository> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="logger">Логгер</param>
        public RecommendationRepository(CoursesDbContext context, ILogger<RecommendationRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetPersonalizedRecommendationsAsync(Guid userId, int count)
        {
            try
            {
                // Получаем интересы пользователя
                var userInterests = await GetUserInterestsAsync(userId);
                
                // Получаем историю просмотров
                var viewHistory = await GetUserViewHistoryAsync(userId, 20);
                var viewedCourseIds = viewHistory.Select(v => v.CourseId).ToList();
                
                // Получаем завершенные курсы
                var completedCourses = await _context.Enrollments
                    .Where(e => e.StudentId == userId && e.Status == "Completed")
                    .Select(e => e.CourseId)
                    .ToListAsync();
                
                // Исключаем курсы, которые пользователь уже просмотрел или завершил
                var excludedCourseIds = viewedCourseIds.Union(completedCourses).ToList();
                
                // Получаем курсы, соответствующие интересам пользователя
                var recommendedCourses = await _context.Courses
                    .Where(c => !excludedCourseIds.Contains(c.Id))
                    .Where(c => c.IsPublished && !c.IsDeleted)
                    .OrderByDescending(c => c.Rating)
                    .Take(count * 2) // Берем с запасом для дальнейшей фильтрации
                    .Select(c => new CourseRecommendationModel
                    {
                        CourseId = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        Author = c.AuthorName,
                        AuthorId = c.AuthorId,
                        Rating = c.Rating,
                        ReviewCount = c.ReviewCount,
                        DifficultyLevel = c.DifficultyLevel,
                        DurationMinutes = c.DurationMinutes,
                        Category = c.Category,
                        Tags = c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        RelevanceScore = 0,
                        RecommendationReason = "Персонализированная рекомендация"
                    })
                    .ToListAsync();
                
                // Вычисляем релевантность на основе интересов пользователя
                foreach (var course in recommendedCourses)
                {
                    decimal relevanceScore = 0;
                    
                    // Проверяем совпадение тегов с интересами
                    foreach (var tag in course.Tags)
                    {
                        if (userInterests.Contains(tag.Trim().ToLower()))
                        {
                            relevanceScore += 0.5m;
                        }
                    }
                    
                    // Учитываем рейтинг курса
                    relevanceScore += course.Rating * 0.3m;
                    
                    course.RelevanceScore = Math.Min(relevanceScore, 5);
                }
                
                // Сортируем по релевантности и возвращаем запрошенное количество
                return recommendedCourses
                    .OrderByDescending(c => c.RelevanceScore)
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении персонализированных рекомендаций для пользователя {UserId}", userId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnViewHistoryAsync(Guid userId, int count)
        {
            try
            {
                // Получаем историю просмотров
                var viewHistory = await GetUserViewHistoryAsync(userId, 10);
                if (!viewHistory.Any())
                {
                    return await GetPopularCoursesAsync(count, 30); // Если нет истории, возвращаем популярные курсы
                }
                
                var viewedCourseIds = viewHistory.Select(v => v.CourseId).ToList();
                
                // Получаем категории и теги просмотренных курсов
                var viewedCourses = await _context.Courses
                    .Where(c => viewedCourseIds.Contains(c.Id))
                    .ToListAsync();
                
                var categories = viewedCourses.Select(c => c.Category).Distinct().ToList();
                var allTags = viewedCourses
                    .SelectMany(c => c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    .Select(t => t.Trim().ToLower())
                    .Distinct()
                    .ToList();
                
                // Получаем курсы с похожими категориями и тегами
                var recommendedCourses = await _context.Courses
                    .Where(c => !viewedCourseIds.Contains(c.Id)) // Исключаем уже просмотренные
                    .Where(c => c.IsPublished && !c.IsDeleted)
                    .Where(c => categories.Contains(c.Category) || 
                           c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Any(t => allTags.Contains(t.Trim().ToLower())))
                    .OrderByDescending(c => c.Rating)
                    .Take(count * 2) // Берем с запасом для дальнейшей фильтрации
                    .Select(c => new CourseRecommendationModel
                    {
                        CourseId = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        Author = c.AuthorName,
                        AuthorId = c.AuthorId,
                        Rating = c.Rating,
                        ReviewCount = c.ReviewCount,
                        DifficultyLevel = c.DifficultyLevel,
                        DurationMinutes = c.DurationMinutes,
                        Category = c.Category,
                        Tags = c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        RelevanceScore = 0,
                        RecommendationReason = "На основе вашей истории просмотров"
                    })
                    .ToListAsync();
                
                // Вычисляем релевантность
                foreach (var course in recommendedCourses)
                {
                    decimal relevanceScore = 0;
                    
                    // Проверяем совпадение категории
                    if (categories.Contains(course.Category))
                    {
                        relevanceScore += 1.0m;
                    }
                    
                    // Проверяем совпадение тегов
                    foreach (var tag in course.Tags)
                    {
                        if (allTags.Contains(tag.Trim().ToLower()))
                        {
                            relevanceScore += 0.5m;
                        }
                    }
                    
                    // Учитываем рейтинг курса
                    relevanceScore += course.Rating * 0.3m;
                    
                    course.RelevanceScore = Math.Min(relevanceScore, 5);
                }
                
                // Сортируем по релевантности и возвращаем запрошенное количество
                return recommendedCourses
                    .OrderByDescending(c => c.RelevanceScore)
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении рекомендаций на основе истории просмотров для пользователя {UserId}", userId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnCompletedCoursesAsync(Guid userId, int count)
        {
            try
            {
                // Получаем завершенные курсы
                var completedCourseIds = await _context.Enrollments
                    .Where(e => e.StudentId == userId && e.Status == "Completed")
                    .Select(e => e.CourseId)
                    .ToListAsync();
                
                if (!completedCourseIds.Any())
                {
                    return await GetPopularCoursesAsync(count, 30); // Если нет завершенных курсов, возвращаем популярные
                }
                
                // Получаем категории и теги завершенных курсов
                var completedCourses = await _context.Courses
                    .Where(c => completedCourseIds.Contains(c.Id))
                    .ToListAsync();
                
                var categories = completedCourses.Select(c => c.Category).Distinct().ToList();
                var allTags = completedCourses
                    .SelectMany(c => c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    .Select(t => t.Trim().ToLower())
                    .Distinct()
                    .ToList();
                
                // Получаем курсы с похожими категориями и тегами
                var recommendedCourses = await _context.Courses
                    .Where(c => !completedCourseIds.Contains(c.Id)) // Исключаем уже завершенные
                    .Where(c => c.IsPublished && !c.IsDeleted)
                    .Where(c => categories.Contains(c.Category) || 
                           c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Any(t => allTags.Contains(t.Trim().ToLower())))
                    .OrderByDescending(c => c.Rating)
                    .Take(count * 2) // Берем с запасом для дальнейшей фильтрации
                    .Select(c => new CourseRecommendationModel
                    {
                        CourseId = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        Author = c.AuthorName,
                        AuthorId = c.AuthorId,
                        Rating = c.Rating,
                        ReviewCount = c.ReviewCount,
                        DifficultyLevel = c.DifficultyLevel,
                        DurationMinutes = c.DurationMinutes,
                        Category = c.Category,
                        Tags = c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        RelevanceScore = 0,
                        RecommendationReason = "На основе завершенных вами курсов"
                    })
                    .ToListAsync();
                
                // Вычисляем релевантность
                foreach (var course in recommendedCourses)
                {
                    decimal relevanceScore = 0;
                    
                    // Проверяем совпадение категории
                    if (categories.Contains(course.Category))
                    {
                        relevanceScore += 1.0m;
                    }
                    
                    // Проверяем совпадение тегов
                    foreach (var tag in course.Tags)
                    {
                        if (allTags.Contains(tag.Trim().ToLower()))
                        {
                            relevanceScore += 0.5m;
                        }
                    }
                    
                    // Учитываем рейтинг курса
                    relevanceScore += course.Rating * 0.3m;
                    
                    course.RelevanceScore = Math.Min(relevanceScore, 5);
                }
                
                // Сортируем по релевантности и возвращаем запрошенное количество
                return recommendedCourses
                    .OrderByDescending(c => c.RelevanceScore)
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении рекомендаций на основе завершенных курсов для пользователя {UserId}", userId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnInterestsAsync(Guid userId, int count)
        {
            try
            {
                // Получаем интересы пользователя
                var userInterests = await GetUserInterestsAsync(userId);
                
                if (!userInterests.Any())
                {
                    return await GetPopularCoursesAsync(count, 30); // Если нет интересов, возвращаем популярные
                }
                
                // Получаем курсы, соответствующие интересам пользователя
                var recommendedCourses = await _context.Courses
                    .Where(c => c.IsPublished && !c.IsDeleted)
                    .Where(c => userInterests.Any(interest => 
                        c.Tags.Contains(interest) || c.Category.ToLower() == interest.ToLower()))
                    .OrderByDescending(c => c.Rating)
                    .Take(count * 2) // Берем с запасом для дальнейшей фильтрации
                    .Select(c => new CourseRecommendationModel
                    {
                        CourseId = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        Author = c.AuthorName,
                        AuthorId = c.AuthorId,
                        Rating = c.Rating,
                        ReviewCount = c.ReviewCount,
                        DifficultyLevel = c.DifficultyLevel,
                        DurationMinutes = c.DurationMinutes,
                        Category = c.Category,
                        Tags = c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        RelevanceScore = 0,
                        RecommendationReason = "На основе ваших интересов"
                    })
                    .ToListAsync();
                
                // Вычисляем релевантность
                foreach (var course in recommendedCourses)
                {
                    decimal relevanceScore = 0;
                    
                    // Проверяем совпадение категории с интересами
                    if (userInterests.Contains(course.Category.ToLower()))
                    {
                        relevanceScore += 1.0m;
                    }
                    
                    // Проверяем совпадение тегов с интересами
                    foreach (var tag in course.Tags)
                    {
                        if (userInterests.Contains(tag.Trim().ToLower()))
                        {
                            relevanceScore += 0.5m;
                        }
                    }
                    
                    // Учитываем рейтинг курса
                    relevanceScore += course.Rating * 0.3m;
                    
                    course.RelevanceScore = Math.Min(relevanceScore, 5);
                }
                
                // Сортируем по релевантности и возвращаем запрошенное количество
                return recommendedCourses
                    .OrderByDescending(c => c.RelevanceScore)
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении рекомендаций на основе интересов для пользователя {UserId}", userId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetSimilarCoursesAsync(Guid courseId, int count)
        {
            try
            {
                // Получаем информацию о курсе
                var course = await _context.Courses
                    .FirstOrDefaultAsync(c => c.Id == courseId);
                
                if (course == null)
                {
                    _logger.LogWarning("Курс с ID {CourseId} не найден", courseId);
                    return new List<CourseRecommendationModel>();
                }
                
                var category = course.Category;
                var tags = course.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim().ToLower())
                    .ToList();
                
                // Получаем похожие курсы
                var similarCourses = await _context.Courses
                    .Where(c => c.Id != courseId) // Исключаем текущий курс
                    .Where(c => c.IsPublished && !c.IsDeleted)
                    .Where(c => c.Category == category || 
                           c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Any(t => tags.Contains(t.Trim().ToLower())))
                    .OrderByDescending(c => c.Rating)
                    .Take(count * 2) // Берем с запасом для дальнейшей фильтрации
                    .Select(c => new CourseRecommendationModel
                    {
                        CourseId = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        Author = c.AuthorName,
                        AuthorId = c.AuthorId,
                        Rating = c.Rating,
                        ReviewCount = c.ReviewCount,
                        DifficultyLevel = c.DifficultyLevel,
                        DurationMinutes = c.DurationMinutes,
                        Category = c.Category,
                        Tags = c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        RelevanceScore = 0,
                        RecommendationReason = "Похожие курсы"
                    })
                    .ToListAsync();
                
                // Вычисляем релевантность
                foreach (var similarCourse in similarCourses)
                {
                    decimal relevanceScore = 0;
                    
                    // Проверяем совпадение категории
                    if (similarCourse.Category == category)
                    {
                        relevanceScore += 1.0m;
                    }
                    
                    // Проверяем совпадение тегов
                    var courseTags = similarCourse.Tags.Select(t => t.Trim().ToLower()).ToList();
                    int matchingTags = courseTags.Count(t => tags.Contains(t));
                    relevanceScore += matchingTags * 0.5m;
                    
                    // Учитываем рейтинг курса
                    relevanceScore += similarCourse.Rating * 0.3m;
                    
                    similarCourse.RelevanceScore = Math.Min(relevanceScore, 5);
                }
                
                // Сортируем по релевантности и возвращаем запрошенное количество
                return similarCourses
                    .OrderByDescending(c => c.RelevanceScore)
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении похожих курсов для курса {CourseId}", courseId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetPopularCoursesAsync(int count, int period)
        {
            try
            {
                var startDate = DateTime.UtcNow.AddDays(-period);
                
                // Получаем популярные курсы на основе количества просмотров и рейтинга
                var popularCourseIds = await _context.CourseViews
                    .Where(v => v.ViewedAt >= startDate)
                    .GroupBy(v => v.CourseId)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .Take(count * 2)
                    .ToListAsync();
                
                var popularCourses = await _context.Courses
                    .Where(c => popularCourseIds.Contains(c.Id))
                    .Where(c => c.IsPublished && !c.IsDeleted)
                    .OrderByDescending(c => c.Rating)
                    .Take(count)
                    .Select(c => new CourseRecommendationModel
                    {
                        CourseId = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        Author = c.AuthorName,
                        AuthorId = c.AuthorId,
                        Rating = c.Rating,
                        ReviewCount = c.ReviewCount,
                        DifficultyLevel = c.DifficultyLevel,
                        DurationMinutes = c.DurationMinutes,
                        Category = c.Category,
                        Tags = c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        RelevanceScore = c.Rating,
                        RecommendationReason = "Популярные курсы"
                    })
                    .ToListAsync();
                
                // Если популярных курсов недостаточно, добавляем курсы с высоким рейтингом
                if (popularCourses.Count < count)
                {
                    var existingIds = popularCourses.Select(c => c.CourseId).ToList();
                    
                    var additionalCourses = await _context.Courses
                        .Where(c => !existingIds.Contains(c.Id))
                        .Where(c => c.IsPublished && !c.IsDeleted)
                        .OrderByDescending(c => c.Rating)
                        .Take(count - popularCourses.Count)
                        .Select(c => new CourseRecommendationModel
                        {
                            CourseId = c.Id,
                            Title = c.Title,
                            Description = c.Description,
                            ImageUrl = c.ImageUrl,
                            Author = c.AuthorName,
                            AuthorId = c.AuthorId,
                            Rating = c.Rating,
                            ReviewCount = c.ReviewCount,
                            DifficultyLevel = c.DifficultyLevel,
                            DurationMinutes = c.DurationMinutes,
                            Category = c.Category,
                            Tags = c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                            RelevanceScore = c.Rating,
                            RecommendationReason = "Высоко оцененные курсы"
                        })
                        .ToListAsync();
                    
                    popularCourses.AddRange(additionalCourses);
                }
                
                return popularCourses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении популярных курсов за период {Period} дней", period);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetNewCoursesAsync(int count, int period)
        {
            try
            {
                var startDate = DateTime.UtcNow.AddDays(-period);
                
                // Получаем новые курсы
                var newCourses = await _context.Courses
                    .Where(c => c.CreatedAt >= startDate)
                    .Where(c => c.IsPublished && !c.IsDeleted)
                    .OrderByDescending(c => c.CreatedAt)
                    .Take(count)
                    .Select(c => new CourseRecommendationModel
                    {
                        CourseId = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        Author = c.AuthorName,
                        AuthorId = c.AuthorId,
                        Rating = c.Rating,
                        ReviewCount = c.ReviewCount,
                        DifficultyLevel = c.DifficultyLevel,
                        DurationMinutes = c.DurationMinutes,
                        Category = c.Category,
                        Tags = c.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        RelevanceScore = 5, // Новые курсы имеют высокую релевантность
                        RecommendationReason = "Новые курсы"
                    })
                    .ToListAsync();
                
                return newCourses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении новых курсов за период {Period} дней", period);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task TrackCourseViewAsync(CourseViewEvent courseViewEvent)
        {
            try
            {
                var courseView = new CourseView
                {
                    UserId = courseViewEvent.UserId,
                    CourseId = courseViewEvent.CourseId,
                    ViewedAt = courseViewEvent.ViewedAt,
                    IpAddress = courseViewEvent.IpAddress,
                    ReferralSource = courseViewEvent.ReferralSource,
                    Device = courseViewEvent.Device
                };
                
                await _context.CourseViews.AddAsync(courseView);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Просмотр курса {CourseId} пользователем {UserId} успешно записан", courseViewEvent.CourseId, courseViewEvent.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при записи просмотра курса {CourseId} пользователем {UserId}", courseViewEvent.CourseId, courseViewEvent.UserId);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseViewEvent>> GetUserViewHistoryAsync(Guid userId, int count)
        {
            try
            {
                var viewHistory = await _context.CourseViews
                    .Where(v => v.UserId == userId)
                    .OrderByDescending(v => v.ViewedAt)
                    .Take(count)
                    .Select(v => new CourseViewEvent
                    {
                        Id = v.Id,
                        UserId = v.UserId,
                        CourseId = v.CourseId,
                        ViewedAt = v.ViewedAt,
                        IpAddress = v.IpAddress,
                        ReferralSource = v.ReferralSource,
                        Device = v.Device
                    })
                    .ToListAsync();
                
                return viewHistory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении истории просмотров пользователя {UserId}", userId);
                return new List<CourseViewEvent>();
            }
        }

        /// <inheritdoc/>
        public async Task UpdateUserInterestsAsync(Guid userId, List<string> interests)
        {
            try
            {
                // Удаляем существующие интересы пользователя
                var existingInterests = await _context.UserInterests
                    .Where(ui => ui.UserId == userId)
                    .ToListAsync();
                
                _context.UserInterests.RemoveRange(existingInterests);
                
                // Добавляем новые интересы
                foreach (var interest in interests.Distinct())
                {
                    await _context.UserInterests.AddAsync(new UserInterest
                    {
                        UserId = userId,
                        Interest = interest.Trim().ToLower(),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                }
                
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Интересы пользователя {UserId} успешно обновлены", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении интересов пользователя {UserId}", userId);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetUserInterestsAsync(Guid userId)
        {
            try
            {
                var interests = await _context.UserInterests
                    .Where(ui => ui.UserId == userId)
                    .Select(ui => ui.Interest)
                    .ToListAsync();
                
                return interests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении интересов пользователя {UserId}", userId);
                return new List<string>();
            }
        }
    }
}