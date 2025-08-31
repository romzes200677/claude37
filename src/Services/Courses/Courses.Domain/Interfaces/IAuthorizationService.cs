using System;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса авторизации
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Проверить, является ли пользователь автором курса
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Результат проверки</returns>
        Task<bool> IsAuthorAsync(Guid userId, Guid courseId);

        /// <summary>
        /// Проверить, является ли пользователь студентом курса
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Результат проверки</returns>
        Task<bool> IsEnrolledAsync(Guid userId, Guid courseId);

        /// <summary>
        /// Проверить, имеет ли пользователь роль администратора
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Результат проверки</returns>
        Task<bool> IsAdminAsync(Guid userId);

        /// <summary>
        /// Проверить, имеет ли пользователь роль модератора
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Результат проверки</returns>
        Task<bool> IsModeratorAsync(Guid userId);

        /// <summary>
        /// Проверить, имеет ли пользователь доступ к курсу
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Результат проверки</returns>
        Task<bool> HasCourseAccessAsync(Guid userId, Guid courseId);

        /// <summary>
        /// Проверить, имеет ли пользователь доступ к уроку
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Результат проверки</returns>
        Task<bool> HasLessonAccessAsync(Guid userId, Guid lessonId);

        /// <summary>
        /// Проверить, имеет ли пользователь доступ к модулю
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <returns>Результат проверки</returns>
        Task<bool> HasModuleAccessAsync(Guid userId, Guid moduleId);
    }
}