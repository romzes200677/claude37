using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса аналитики
    /// </summary>
    public interface IAnalyticsService
    {
        /// <summary>
        /// Получить статистику по курсу
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Статистика по курсу</returns>
        Task<CourseStatisticsModel> GetCourseStatisticsAsync(Guid courseId);

        /// <summary>
        /// Получить статистику по студенту
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Статистика по студенту</returns>
        Task<StudentStatisticsModel> GetStudentStatisticsAsync(Guid studentId);

        /// <summary>
        /// Получить статистику по автору
        /// </summary>
        /// <param name="authorId">Идентификатор автора</param>
        /// <returns>Статистика по автору</returns>
        Task<AuthorStatisticsModel> GetAuthorStatisticsAsync(Guid authorId);

        /// <summary>
        /// Получить популярные курсы
        /// </summary>
        /// <param name="count">Количество</param>
        /// <returns>Список популярных курсов</returns>
        Task<List<PopularCourseModel>> GetPopularCoursesAsync(int count = 10);

        /// <summary>
        /// Получить статистику по завершению курсов
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Статистика по завершению курса</returns>
        Task<CourseCompletionStatisticsModel> GetCourseCompletionStatisticsAsync(Guid courseId);

        /// <summary>
        /// Записать событие аналитики
        /// </summary>
        /// <param name="eventType">Тип события</param>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="metadata">Метаданные</param>
        /// <returns>Задача</returns>
        Task TrackEventAsync(string eventType, Guid entityId, Guid userId, Dictionary<string, string> metadata = null);
    }
}