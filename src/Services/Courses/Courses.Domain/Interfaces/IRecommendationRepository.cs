using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория рекомендаций
    /// </summary>
    public interface IRecommendationRepository
    {
        /// <summary>
        /// Получить персонализированные рекомендации курсов для пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        Task<List<CourseRecommendationModel>> GetPersonalizedRecommendationsAsync(Guid userId, int count);

        /// <summary>
        /// Получить рекомендации курсов на основе истории просмотров пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnViewHistoryAsync(Guid userId, int count);

        /// <summary>
        /// Получить рекомендации курсов на основе завершенных курсов пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnCompletedCoursesAsync(Guid userId, int count);

        /// <summary>
        /// Получить рекомендации курсов на основе интересов пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        Task<List<CourseRecommendationModel>> GetRecommendationsBasedOnInterestsAsync(Guid userId, int count);

        /// <summary>
        /// Получить похожие курсы
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список похожих курсов</returns>
        Task<List<CourseRecommendationModel>> GetSimilarCoursesAsync(Guid courseId, int count);

        /// <summary>
        /// Получить популярные курсы
        /// </summary>
        /// <param name="count">Количество курсов</param>
        /// <param name="period">Период (в днях)</param>
        /// <returns>Список популярных курсов</returns>
        Task<List<CourseRecommendationModel>> GetPopularCoursesAsync(int count, int period);

        /// <summary>
        /// Получить новые курсы
        /// </summary>
        /// <param name="count">Количество курсов</param>
        /// <param name="period">Период (в днях)</param>
        /// <returns>Список новых курсов</returns>
        Task<List<CourseRecommendationModel>> GetNewCoursesAsync(int count, int period);

        /// <summary>
        /// Записать событие просмотра курса
        /// </summary>
        /// <param name="courseViewEvent">Событие просмотра курса</param>
        /// <returns>Задача</returns>
        Task TrackCourseViewAsync(CourseViewEvent courseViewEvent);

        /// <summary>
        /// Получить историю просмотров пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Количество записей</param>
        /// <returns>Список событий просмотра</returns>
        Task<List<CourseViewEvent>> GetUserViewHistoryAsync(Guid userId, int count);

        /// <summary>
        /// Обновить интересы пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="interests">Список интересов</param>
        /// <returns>Задача</returns>
        Task UpdateUserInterestsAsync(Guid userId, List<string> interests);

        /// <summary>
        /// Получить интересы пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Список интересов</returns>
        Task<List<string>> GetUserInterestsAsync(Guid userId);
    }
}