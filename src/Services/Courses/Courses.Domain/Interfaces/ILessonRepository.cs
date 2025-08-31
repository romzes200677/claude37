using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория уроков
    /// </summary>
    public interface ILessonRepository : IRepository<Lesson>
    {
        /// <summary>
        /// Получить уроки по идентификатору модуля
        /// </summary>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <returns>Коллекция уроков</returns>
        Task<IEnumerable<Lesson>> GetLessonsByModuleIdAsync(Guid moduleId);

        /// <summary>
        /// Получить урок с материалами
        /// </summary>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Урок с материалами</returns>
        Task<Lesson> GetLessonWithMaterialsAsync(Guid lessonId);

        /// <summary>
        /// Обновить порядок уроков
        /// </summary>
        /// <param name="lessonIds">Список идентификаторов уроков в новом порядке</param>
        /// <returns>Задача</returns>
        Task UpdateLessonsOrderAsync(IEnumerable<Guid> lessonIds);

        /// <summary>
        /// Получить уроки по типу
        /// </summary>
        /// <param name="lessonType">Тип урока</param>
        /// <returns>Коллекция уроков</returns>
        Task<IEnumerable<Lesson>> GetLessonsByTypeAsync(string lessonType);
    }
}