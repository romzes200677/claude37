using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория курсов
    /// </summary>
    public interface ICourseRepository : IRepository<Course>
    {
        /// <summary>
        /// Получить курсы по идентификатору автора
        /// </summary>
        /// <param name="authorId">Идентификатор автора</param>
        /// <returns>Коллекция курсов</returns>
        Task<IEnumerable<Course>> GetCoursesByAuthorIdAsync(Guid authorId);

        /// <summary>
        /// Получить опубликованные курсы
        /// </summary>
        /// <returns>Коллекция курсов</returns>
        Task<IEnumerable<Course>> GetPublishedCoursesAsync();

        /// <summary>
        /// Получить курс с модулями и уроками
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Курс с модулями и уроками</returns>
        Task<Course> GetCourseWithModulesAndLessonsAsync(Guid courseId);

        /// <summary>
        /// Получить курсы, на которые записан студент
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Коллекция курсов</returns>
        Task<IEnumerable<Course>> GetEnrolledCoursesAsync(Guid studentId);

        /// <summary>
        /// Получить курсы по уровню сложности
        /// </summary>
        /// <param name="difficultyLevel">Уровень сложности</param>
        /// <returns>Коллекция курсов</returns>
        Task<IEnumerable<Course>> GetCoursesByDifficultyLevelAsync(string difficultyLevel);

        /// <summary>
        /// Поиск курсов по названию или описанию
        /// </summary>
        /// <param name="searchTerm">Поисковый запрос</param>
        /// <returns>Коллекция курсов</returns>
        Task<IEnumerable<Course>> SearchCoursesAsync(string searchTerm);
    }
}