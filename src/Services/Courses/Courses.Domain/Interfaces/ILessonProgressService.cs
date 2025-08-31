using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса прогресса по урокам
    /// </summary>
    public interface ILessonProgressService
    {
        /// <summary>
        /// Получить прогресс по идентификатору
        /// </summary>
        /// <param name="progressId">Идентификатор прогресса</param>
        /// <returns>Прогресс</returns>
        Task<LessonProgress> GetProgressByIdAsync(Guid progressId);

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

        /// <summary>
        /// Начать урок
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Созданный прогресс</returns>
        Task<LessonProgress> StartLessonAsync(Guid studentId, Guid lessonId);

        /// <summary>
        /// Обновить прогресс по уроку
        /// </summary>
        /// <param name="progressId">Идентификатор прогресса</param>
        /// <param name="completionPercentage">Процент завершения</param>
        /// <returns>Обновленный прогресс</returns>
        Task<LessonProgress> UpdateProgressAsync(Guid progressId, int completionPercentage);

        /// <summary>
        /// Завершить урок
        /// </summary>
        /// <param name="progressId">Идентификатор прогресса</param>
        /// <param name="score">Оценка</param>
        /// <returns>Обновленный прогресс</returns>
        Task<LessonProgress> CompleteLessonAsync(Guid progressId, decimal? score);

        /// <summary>
        /// Получить общий прогресс студента по курсу в процентах
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Процент завершения</returns>
        Task<int> GetOverallCourseProgressPercentageAsync(Guid studentId, Guid courseId);
    }
}