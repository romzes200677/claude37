using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса рекомендаций
    /// </summary>
    public interface IRecommendationService
    {
        /// <summary>
        /// Получить персонализированные рекомендации курсов для пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        Task<List<CourseRecommendationModel>> GetPersonalizedRecommendationsAsync(Guid userId, int count = 5);

        /// <summary>
        /// Получить рекомендации курсов на основе истории просмотров пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnViewHistoryAsync(Guid userId, int count = 5);

        /// <summary>
        /// Получить рекомендации курсов на основе завершенных курсов пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnCompletedCoursesAsync(Guid userId, int count = 5);

        /// <summary>
        /// Получить рекомендации курсов на основе интересов пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnInterestsAsync(Guid userId, int count = 5);

        /// <summary>
        /// Получить похожие курсы
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список похожих курсов</returns>
        Task<List<CourseRecommendationModel>> GetSimilarCoursesAsync(Guid courseId, int count = 5);

        /// <summary>
        /// Получить популярные курсы
        /// </summary>
        /// <param name="count">Количество курсов</param>
        /// <param name="period">Период (в днях)</param>
        /// <returns>Список популярных курсов</returns>
        Task<List<CourseRecommendationModel>> GetPopularCoursesAsync(int count = 10, int period = 30);

        /// <summary>
        /// Получить новые курсы
        /// </summary>
        /// <param name="count">Количество курсов</param>
        /// <param name="period">Период (в днях)</param>
        /// <returns>Список новых курсов</returns>
        Task<List<CourseRecommendationModel>> GetNewCoursesAsync(int count = 10, int period = 30);

        /// <summary>
        /// Записать событие просмотра курса
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Задача</returns>
        Task TrackCourseViewAsync(Guid userId, Guid courseId);

        /// <summary>
        /// Обновить интересы пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="interests">Список интересов</param>
        /// <returns>Задача</returns>
        Task UpdateUserInterestsAsync(Guid userId, List<string> interests);
    }
}