using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис рекомендаций
    /// </summary>
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly ILogger<RecommendationService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="recommendationRepository">Репозиторий рекомендаций</param>
        /// <param name="logger">Логгер</param>
        public RecommendationService(
            IRecommendationRepository recommendationRepository,
            ILogger<RecommendationService> logger)
        {
            _recommendationRepository = recommendationRepository ?? throw new ArgumentNullException(nameof(recommendationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetPersonalizedRecommendationsAsync(Guid userId, int count = 5)
        {
            try
            {
                _logger.LogInformation("Получение персонализированных рекомендаций для пользователя {UserId}", userId);
                return await _recommendationRepository.GetPersonalizedRecommendationsAsync(userId, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении персонализированных рекомендаций для пользователя {UserId}", userId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnViewHistoryAsync(Guid userId, int count = 5)
        {
            try
            {
                _logger.LogInformation("Получение рекомендаций на основе истории просмотров для пользователя {UserId}", userId);
                return await _recommendationRepository.GetRecommendationsBasedOnViewHistoryAsync(userId, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении рекомендаций на основе истории просмотров для пользователя {UserId}", userId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnCompletedCoursesAsync(Guid userId, int count = 5)
        {
            try
            {
                _logger.LogInformation("Получение рекомендаций на основе завершенных курсов для пользователя {UserId}", userId);
                return await _recommendationRepository.GetRecommendationsBasedOnCompletedCoursesAsync(userId, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении рекомендаций на основе завершенных курсов для пользователя {UserId}", userId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnInterestsAsync(Guid userId, int count = 5)
        {
            try
            {
                _logger.LogInformation("Получение рекомендаций на основе интересов для пользователя {UserId}", userId);
                return await _recommendationRepository.GetRecommendationsBasedOnInterestsAsync(userId, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении рекомендаций на основе интересов для пользователя {UserId}", userId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetSimilarCoursesAsync(Guid courseId, int count = 5)
        {
            try
            {
                _logger.LogInformation("Получение похожих курсов для курса {CourseId}", courseId);
                return await _recommendationRepository.GetSimilarCoursesAsync(courseId, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении похожих курсов для курса {CourseId}", courseId);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetPopularCoursesAsync(int count = 10, int period = 30)
        {
            try
            {
                _logger.LogInformation("Получение популярных курсов за период {Period} дней", period);
                return await _recommendationRepository.GetPopularCoursesAsync(count, period);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении популярных курсов за период {Period} дней", period);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<List<CourseRecommendationModel>> GetNewCoursesAsync(int count = 10, int period = 30)
        {
            try
            {
                _logger.LogInformation("Получение новых курсов за период {Period} дней", period);
                return await _recommendationRepository.GetNewCoursesAsync(count, period);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении новых курсов за период {Period} дней", period);
                return new List<CourseRecommendationModel>();
            }
        }

        /// <inheritdoc/>
        public async Task TrackCourseViewAsync(Guid userId, Guid courseId)
        {
            try
            {
                _logger.LogInformation("Запись просмотра курса {CourseId} пользователем {UserId}", courseId, userId);
                
                var courseViewEvent = new CourseViewEvent
                {
                    UserId = userId,
                    CourseId = courseId,
                    ViewedAt = DateTime.UtcNow,
                    IpAddress = "Unknown", // В реальном приложении получаем из контекста запроса
                    ReferralSource = "Unknown", // В реальном приложении получаем из контекста запроса
                    Device = "Unknown" // В реальном приложении получаем из контекста запроса
                };
                
                await _recommendationRepository.TrackCourseViewAsync(courseViewEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при записи просмотра курса {CourseId} пользователем {UserId}", courseId, userId);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task UpdateUserInterestsAsync(Guid userId, List<string> interests)
        {
            try
            {
                _logger.LogInformation("Обновление интересов пользователя {UserId}", userId);
                
                // Нормализуем интересы (приводим к нижнему регистру, удаляем дубликаты и пустые значения)
                var normalizedInterests = interests
                    .Where(i => !string.IsNullOrWhiteSpace(i))
                    .Select(i => i.Trim().ToLower())
                    .Distinct()
                    .ToList();
                
                await _recommendationRepository.UpdateUserInterestsAsync(userId, normalizedInterests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении интересов пользователя {UserId}", userId);
                throw;
            }
        }
    }
}