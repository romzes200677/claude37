using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса курсов
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// Получить все курсы
        /// </summary>
        /// <returns>Коллекция курсов</returns>
        Task<IEnumerable<Course>> GetAllCoursesAsync();

        /// <summary>
        /// Получить опубликованные курсы
        /// </summary>
        /// <returns>Коллекция курсов</returns>
        Task<IEnumerable<Course>> GetPublishedCoursesAsync();

        /// <summary>
        /// Получить курс по идентификатору
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Курс</returns>
        Task<Course> GetCourseByIdAsync(Guid courseId);

        /// <summary>
        /// Получить курс с модулями и уроками
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Курс с модулями и уроками</returns>
        Task<Course> GetCourseWithModulesAndLessonsAsync(Guid courseId);

        /// <summary>
        /// Создать курс
        /// </summary>
        /// <param name="course">Курс</param>
        /// <returns>Созданный курс</returns>
        Task<Course> CreateCourseAsync(Course course);

        /// <summary>
        /// Обновить курс
        /// </summary>
        /// <param name="course">Курс</param>
        /// <returns>Обновленный курс</returns>
        Task<Course> UpdateCourseAsync(Course course);

        /// <summary>
        /// Удалить курс
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Задача</returns>
        Task DeleteCourseAsync(Guid courseId);

        /// <summary>
        /// Опубликовать курс
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Опубликованный курс</returns>
        Task<Course> PublishCourseAsync(Guid courseId);

        /// <summary>
        /// Архивировать курс
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Архивированный курс</returns>
        Task<Course> ArchiveCourseAsync(Guid courseId);

        /// <summary>
        /// Получить курсы по идентификатору автора
        /// </summary>
        /// <param name="authorId">Идентификатор автора</param>
        /// <returns>Коллекция курсов</returns>
        Task<IEnumerable<Course>> GetCoursesByAuthorIdAsync(Guid authorId);

        /// <summary>
        /// Поиск курсов
        /// </summary>
        /// <param name="searchTerm">Поисковый запрос</param>
        /// <returns>Коллекция курсов</returns>
        Task<IEnumerable<Course>> SearchCoursesAsync(string searchTerm);
    }
}