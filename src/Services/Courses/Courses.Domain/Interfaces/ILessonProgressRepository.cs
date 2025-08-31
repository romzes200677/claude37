using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория прогресса по урокам
    /// </summary>
    public interface ILessonProgressRepository : IRepository<LessonProgress>
    {
        /// <summary>
        /// Получить прогресс по идентификатору студента
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Коллекция прогресса</returns>
        Task<IEnumerable<LessonProgress>> GetProgressByStudentIdAsync(Guid studentId);

        /// <summary>
        /// Получить прогресс по идентификатору урока
        /// </summary>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Коллекция прогресса</returns>
        Task<IEnumerable<LessonProgress>> GetProgressByLessonIdAsync(Guid lessonId);

        /// <summary>
        /// Получить прогресс по идентификатору студента и урока
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Прогресс</returns>
        Task<LessonProgress> GetProgressByStudentAndLessonIdAsync(Guid studentId, Guid lessonId);

        /// <summary>
        /// Получить прогресс студента по всем урокам модуля
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <returns>Коллекция прогресса</returns>
        Task<IEnumerable<LessonProgress>> GetProgressByStudentAndModuleIdAsync(Guid studentId, Guid moduleId);

        /// <summary>
        /// Получить прогресс студента по всем урокам курса
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Коллекция прогресса</returns>
        Task<IEnumerable<LessonProgress>> GetProgressByStudentAndCourseIdAsync(Guid studentId, Guid courseId);
    }
}