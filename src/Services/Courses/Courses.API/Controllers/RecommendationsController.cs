using Microsoft.AspNetCore.Mvc;
using Courses.Domain.Interfaces;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Common.Controllers;
using Common.Models;

namespace Courses.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : BaseApiController
    {
        private readonly IRecommendationService _recommendationService;
        private readonly ILogger<RecommendationsController> _logger;

        public RecommendationsController(
            IRecommendationService recommendationService,
            ILogger<RecommendationsController> logger)
        {
            _recommendationService = recommendationService;
            _logger = logger;
        }

        /// <summary>
        /// Получить персонализированные рекомендации курсов для пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="limit">Максимальное количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        [HttpGet("personalized/{userId}")]
        public async Task<IActionResult> GetPersonalizedRecommendations(Guid userId, [FromQuery] int limit = 10)
        {
            try
            {
                var recommendations = await _recommendationService.GetPersonalizedRecommendationsAsync(userId, limit);
                return Success(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении персонализированных рекомендаций для пользователя {UserId}", userId);
                return Error("Ошибка при получении рекомендаций");
            }
        }

        /// <summary>
        /// Получить рекомендации на основе истории просмотров пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="limit">Максимальное количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        [HttpGet("view-history/{userId}")]
        public async Task<IActionResult> GetRecommendationsBasedOnViewHistory(Guid userId, [FromQuery] int limit = 10)
        {
            try
            {
                var recommendations = await _recommendationService.GetRecommendationsBasedOnViewHistoryAsync(userId, limit);
                return Success(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении рекомендаций на основе истории просмотров для пользователя {UserId}", userId);
                return Error("Ошибка при получении рекомендаций");
            }
        }

        /// <summary>
        /// Получить рекомендации на основе завершенных курсов пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="limit">Максимальное количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        [HttpGet("completed-courses/{userId}")]
        public async Task<IActionResult> GetRecommendationsBasedOnCompletedCourses(Guid userId, [FromQuery] int limit = 10)
        {
            try
            {
                var recommendations = await _recommendationService.GetRecommendationsBasedOnCompletedCoursesAsync(userId, limit);
                return Success(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении рекомендаций на основе завершенных курсов для пользователя {UserId}", userId);
                return Error("Ошибка при получении рекомендаций");
            }
        }

        /// <summary>
        /// Получить рекомендации на основе интересов пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="limit">Максимальное количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        [HttpGet("interests/{userId}")]
        public async Task<IActionResult> GetRecommendationsBasedOnInterests(Guid userId, [FromQuery] int limit = 10)
        {
            try
            {
                var recommendations = await _recommendationService.GetRecommendationsBasedOnInterestsAsync(userId, limit);
                return Success(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении рекомендаций на основе интересов для пользователя {UserId}", userId);
                return Error("Ошибка при получении рекомендаций");
            }
        }

        /// <summary>
        /// Получить похожие курсы
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="limit">Максимальное количество рекомендаций</param>
        /// <returns>Список похожих курсов</returns>
        [HttpGet("similar/{courseId}")]
        public async Task<IActionResult> GetSimilarCourses(Guid courseId, [FromQuery] int limit = 10)
        {
            try
            {
                var recommendations = await _recommendationService.GetSimilarCoursesAsync(courseId, limit);
                return Success(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении похожих курсов для курса {CourseId}", courseId);
                return Error("Ошибка при получении похожих курсов");
            }
        }

        /// <summary>
        /// Получить популярные курсы
        /// </summary>
        /// <param name="limit">Максимальное количество курсов</param>
        /// <param name="days">Количество дней для анализа популярности</param>
        /// <returns>Список популярных курсов</returns>
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularCourses([FromQuery] int limit = 10, [FromQuery] int days = 30)
        {
            try
            {
                var recommendations = await _recommendationService.GetPopularCoursesAsync(limit, days);
                return Success(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении популярных курсов");
                return Error("Ошибка при получении популярных курсов");
            }
        }

        /// <summary>
        /// Получить новые курсы
        /// </summary>
        /// <param name="limit">Максимальное количество курсов</param>
        /// <param name="days">Количество дней для определения новых курсов</param>
        /// <returns>Список новых курсов</returns>
        [HttpGet("new")]
        public async Task<IActionResult> GetNewCourses([FromQuery] int limit = 10, [FromQuery] int days = 30)
        {
            try
            {
                var recommendations = await _recommendationService.GetNewCoursesAsync(limit, days);
                return Success(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении новых курсов");
                return Error("Ошибка при получении новых курсов");
            }
        }

        /// <summary>
        /// Отслеживание просмотра курса пользователем
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Результат операции</returns>
        [HttpPost("track-view/{userId}/{courseId}")]
        public async Task<IActionResult> TrackCourseView(Guid userId, Guid courseId)
        {
            try
            {
                await _recommendationService.TrackCourseViewAsync(userId, courseId);
                return Success(null, "Просмотр курса успешно отслежен");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при отслеживании просмотра курса {CourseId} пользователем {UserId}", courseId, userId);
                return Error("Ошибка при отслеживании просмотра курса");
            }
        }

        /// <summary>
        /// Обновление интересов пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="interests">Список интересов</param>
        /// <returns>Результат операции</returns>
        [HttpPost("update-interests/{userId}")]
        public async Task<IActionResult> UpdateUserInterests(Guid userId, [FromBody] List<string> interests)
        {
            try
            {
                await _recommendationService.UpdateUserInterestsAsync(userId, interests);
                return Success(null, "Интересы пользователя успешно обновлены");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении интересов пользователя {UserId}", userId);
                return Error("Ошибка при обновлении интересов пользователя");
            }
        }
    }
}