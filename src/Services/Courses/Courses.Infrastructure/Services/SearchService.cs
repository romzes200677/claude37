using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис поиска курсов
    /// </summary>
    public class SearchService : ISearchService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogService _logService;
        private readonly ILogger<SearchService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="courseRepository">Репозиторий курсов</param>
        /// <param name="enrollmentRepository">Репозиторий зачислений</param>
        /// <param name="reviewRepository">Репозиторий отзывов</param>
        /// <param name="logService">Сервис логирования</param>
        /// <param name="logger">Логгер</param>
        public SearchService(
            ICourseRepository courseRepository,
            IEnrollmentRepository enrollmentRepository,
            IReviewRepository reviewRepository,
            ILogService logService,
            ILogger<SearchService> logger)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Поиск курсов
        /// </summary>
        /// <param name="query">Поисковый запрос</param>
        /// <param name="paginationModel">Модель пагинации</param>
        /// <returns>Результаты поиска</returns>
        public async Task<PaginationResponseModel<CourseViewModel>> SearchCoursesAsync(string query, PaginationRequestModel paginationModel)
        {
            try
            {
                _logService.Information("Поиск курсов по запросу: {Query}", query);

                // Получаем все курсы
                var allCourses = await _courseRepository.GetAllAsync();

                // Фильтруем курсы по поисковому запросу
                var filteredCourses = allCourses
                    .Where(c => c.IsPublished && c.IsActive) // Только опубликованные и активные курсы
                    .Where(c => 
                        c.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        c.Description.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        c.ShortDescription.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        c.Tags.Any(t => t.Contains(query, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                // Применяем сортировку
                IEnumerable<Course> sortedCourses = paginationModel.SortBy?.ToLower() switch
                {
                    "title" => paginationModel.SortDirection?.ToLower() == "desc" 
                        ? filteredCourses.OrderByDescending(c => c.Title)
                        : filteredCourses.OrderBy(c => c.Title),
                    "date" => paginationModel.SortDirection?.ToLower() == "desc" 
                        ? filteredCourses.OrderByDescending(c => c.CreatedAt)
                        : filteredCourses.OrderBy(c => c.CreatedAt),
                    "rating" => paginationModel.SortDirection?.ToLower() == "desc" 
                        ? filteredCourses.OrderByDescending(c => c.AverageRating)
                        : filteredCourses.OrderBy(c => c.AverageRating),
                    _ => filteredCourses.OrderByDescending(c => c.Relevance(query))
                };

                // Применяем пагинацию
                var totalCount = sortedCourses.Count();
                var pageSize = paginationModel.PageSize > 0 ? paginationModel.PageSize : 10;
                var pageNumber = paginationModel.PageNumber > 0 ? paginationModel.PageNumber : 1;
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                var pagedCourses = sortedCourses
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Преобразуем в модель представления
                var courseViewModels = new List<CourseViewModel>();
                foreach (var course in pagedCourses)
                {
                    var enrollmentCount = await _enrollmentRepository.GetEnrollmentCountByCourseIdAsync(course.Id);
                    var reviews = await _reviewRepository.GetReviewsByCourseIdAsync(course.Id);
                    var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

                    courseViewModels.Add(new CourseViewModel
                    {
                        Id = course.Id,
                        Title = course.Title,
                        ShortDescription = course.ShortDescription,
                        ImageUrl = course.ImageUrl,
                        AuthorId = course.AuthorId,
                        AuthorName = course.AuthorName,
                        Price = course.Price,
                        DiscountPrice = course.DiscountPrice,
                        Duration = course.Duration,
                        Level = course.Level,
                        AverageRating = averageRating,
                        ReviewCount = reviews.Count(),
                        EnrollmentCount = enrollmentCount,
                        Tags = course.Tags,
                        CreatedAt = course.CreatedAt,
                        UpdatedAt = course.UpdatedAt
                    });
                }

                return new PaginationResponseModel<CourseViewModel>
                {
                    Items = courseViewModels,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    HasPreviousPage = pageNumber > 1,
                    HasNextPage = pageNumber < totalPages
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при поиске курсов по запросу: {Query}", query);
                throw;
            }
        }

        /// <summary>
        /// Индексировать курс
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Результат операции</returns>
        public async Task<bool> IndexCourseAsync(Guid courseId)
        {
            try
            {
                _logService.Information("Индексация курса: {CourseId}", courseId);

                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    _logService.Warning("Курс с идентификатором {CourseId} не найден", courseId);
                    return false;
                }

                // В реальном приложении здесь была бы логика индексации курса в поисковом движке
                // Например, добавление в Elasticsearch, Solr или другую поисковую систему
                // Для демонстрации просто возвращаем true

                _logService.Information("Курс {CourseId} успешно индексирован", courseId);
                return true;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при индексации курса: {CourseId}", courseId);
                return false;
            }
        }

        /// <summary>
        /// Удалить курс из индекса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Результат операции</returns>
        public async Task<bool> RemoveCourseFromIndexAsync(Guid courseId)
        {
            try
            {
                _logService.Information("Удаление курса из индекса: {CourseId}", courseId);

                // В реальном приложении здесь была бы логика удаления курса из поискового индекса
                // Например, удаление из Elasticsearch, Solr или другой поисковой системы
                // Для демонстрации просто возвращаем true

                _logService.Information("Курс {CourseId} успешно удален из индекса", courseId);
                return true;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при удалении курса из индекса: {CourseId}", courseId);
                return false;
            }
        }

        /// <summary>
        /// Получить рекомендации курсов для пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        public async Task<List<CourseViewModel>> GetCourseRecommendationsAsync(Guid userId, int count = 5)
        {
            try
            {
                _logService.Information("Получение рекомендаций курсов для пользователя: {UserId}", userId);

                // Получаем зачисления пользователя
                var userEnrollments = await _enrollmentRepository.GetEnrollmentsByStudentIdAsync(userId);
                var enrolledCourseIds = userEnrollments.Select(e => e.CourseId).ToList();

                // Получаем все курсы
                var allCourses = await _courseRepository.GetAllAsync();
                var availableCourses = allCourses
                    .Where(c => c.IsPublished && c.IsActive) // Только опубликованные и активные курсы
                    .Where(c => !enrolledCourseIds.Contains(c.Id)) // Исключаем курсы, на которые пользователь уже записан
                    .ToList();

                // В реальном приложении здесь была бы сложная логика рекомендаций
                // Например, на основе истории просмотров, предпочтений, похожих курсов и т.д.
                // Для демонстрации просто возвращаем случайные курсы

                var random = new Random();
                var recommendedCourses = availableCourses
                    .OrderBy(c => random.Next())
                    .Take(count)
                    .ToList();

                // Преобразуем в модель представления
                var courseViewModels = new List<CourseViewModel>();
                foreach (var course in recommendedCourses)
                {
                    var enrollmentCount = await _enrollmentRepository.GetEnrollmentCountByCourseIdAsync(course.Id);
                    var reviews = await _reviewRepository.GetReviewsByCourseIdAsync(course.Id);
                    var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

                    courseViewModels.Add(new CourseViewModel
                    {
                        Id = course.Id,
                        Title = course.Title,
                        ShortDescription = course.ShortDescription,
                        ImageUrl = course.ImageUrl,
                        AuthorId = course.AuthorId,
                        AuthorName = course.AuthorName,
                        Price = course.Price,
                        DiscountPrice = course.DiscountPrice,
                        Duration = course.Duration,
                        Level = course.Level,
                        AverageRating = averageRating,
                        ReviewCount = reviews.Count(),
                        EnrollmentCount = enrollmentCount,
                        Tags = course.Tags,
                        CreatedAt = course.CreatedAt,
                        UpdatedAt = course.UpdatedAt
                    });
                }

                return courseViewModels;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении рекомендаций курсов для пользователя: {UserId}", userId);
                throw;
            }
        }

        /// <summary>
        /// Получить похожие курсы
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="count">Количество похожих курсов</param>
        /// <returns>Список похожих курсов</returns>
        public async Task<List<CourseViewModel>> GetSimilarCoursesAsync(Guid courseId, int count = 5)
        {
            try
            {
                _logService.Information("Получение похожих курсов для курса: {CourseId}", courseId);

                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    _logService.Warning("Курс с идентификатором {CourseId} не найден", courseId);
                    return new List<CourseViewModel>();
                }

                // Получаем все курсы
                var allCourses = await _courseRepository.GetAllAsync();
                var availableCourses = allCourses
                    .Where(c => c.IsPublished && c.IsActive) // Только опубликованные и активные курсы
                    .Where(c => c.Id != courseId) // Исключаем текущий курс
                    .ToList();

                // В реальном приложении здесь была бы сложная логика определения похожих курсов
                // Например, на основе тегов, категорий, содержания и т.д.
                // Для демонстрации используем простую логику на основе тегов

                var courseTags = course.Tags;
                var similarCourses = availableCourses
                    .Select(c => new
                    {
                        Course = c,
                        Similarity = CalculateSimilarity(courseTags, c.Tags)
                    })
                    .OrderByDescending(c => c.Similarity)
                    .Take(count)
                    .Select(c => c.Course)
                    .ToList();

                // Преобразуем в модель представления
                var courseViewModels = new List<CourseViewModel>();
                foreach (var similarCourse in similarCourses)
                {
                    var enrollmentCount = await _enrollmentRepository.GetEnrollmentCountByCourseIdAsync(similarCourse.Id);
                    var reviews = await _reviewRepository.GetReviewsByCourseIdAsync(similarCourse.Id);
                    var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

                    courseViewModels.Add(new CourseViewModel
                    {
                        Id = similarCourse.Id,
                        Title = similarCourse.Title,
                        ShortDescription = similarCourse.ShortDescription,
                        ImageUrl = similarCourse.ImageUrl,
                        AuthorId = similarCourse.AuthorId,
                        AuthorName = similarCourse.AuthorName,
                        Price = similarCourse.Price,
                        DiscountPrice = similarCourse.DiscountPrice,
                        Duration = similarCourse.Duration,
                        Level = similarCourse.Level,
                        AverageRating = averageRating,
                        ReviewCount = reviews.Count(),
                        EnrollmentCount = enrollmentCount,
                        Tags = similarCourse.Tags,
                        CreatedAt = similarCourse.CreatedAt,
                        UpdatedAt = similarCourse.UpdatedAt
                    });
                }

                return courseViewModels;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении похожих курсов для курса: {CourseId}", courseId);
                throw;
            }
        }

        /// <summary>
        /// Расчет схожести курсов на основе тегов
        /// </summary>
        /// <param name="tags1">Теги первого курса</param>
        /// <param name="tags2">Теги второго курса</param>
        /// <returns>Коэффициент схожести</returns>
        private double CalculateSimilarity(List<string> tags1, List<string> tags2)
        {
            if (tags1 == null || tags2 == null || !tags1.Any() || !tags2.Any())
                return 0;

            // Используем коэффициент Жаккара для определения схожести
            // Жаккар = |A ∩ B| / |A ∪ B|
            var intersection = tags1.Intersect(tags2, StringComparer.OrdinalIgnoreCase).Count();
            var union = tags1.Union(tags2, StringComparer.OrdinalIgnoreCase).Count();

            return (double)intersection / union;
        }
    }

    /// <summary>
    /// Расширения для класса Course
    /// </summary>
    public static class CourseExtensions
    {
        /// <summary>
        /// Расчет релевантности курса для поискового запроса
        /// </summary>
        /// <param name="course">Курс</param>
        /// <param name="query">Поисковый запрос</param>
        /// <returns>Релевантность</returns>
        public static double Relevance(this Course course, string query)
        {
            if (string.IsNullOrEmpty(query))
                return 0;

            double relevance = 0;

            // Проверяем вхождение запроса в различные поля курса с разными весами
            if (course.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                relevance += 10; // Наибольший вес для заголовка

            if (course.ShortDescription.Contains(query, StringComparison.OrdinalIgnoreCase))
                relevance += 5; // Средний вес для краткого описания

            if (course.Description.Contains(query, StringComparison.OrdinalIgnoreCase))
                relevance += 3; // Меньший вес для полного описания

            // Проверяем вхождение запроса в теги
            foreach (var tag in course.Tags)
            {
                if (tag.Contains(query, StringComparison.OrdinalIgnoreCase))
                    relevance += 7; // Высокий вес для тегов
            }

            // Учитываем рейтинг и количество зачислений
            relevance += course.AverageRating * 0.5;
            relevance += Math.Min(course.EnrollmentCount / 100.0, 5); // Ограничиваем влияние количества зачислений

            return relevance;
        }
    }
}