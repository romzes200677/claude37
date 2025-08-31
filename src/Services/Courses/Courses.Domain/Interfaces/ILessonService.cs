using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса уроков
    /// </summary>
    public interface ILessonService
    {
        /// <summary>
        /// Получить урок по идентификатору
        /// </summary>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Урок</returns>
        Task<Lesson> GetLessonByIdAsync(Guid lessonId);

        /// <summary>
        /// Получить урок с материалами
        /// </summary>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Урок с материалами</returns>
        Task<Lesson> GetLessonWithMaterialsAsync(Guid lessonId);

        /// <summary>
        /// Получить уроки по идентификатору модуля
        /// </summary>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <returns>Коллекция уроков</returns>
        Task<IEnumerable<Lesson>> GetLessonsByModuleIdAsync(Guid moduleId);

        /// <summary>
        /// Создать урок
        /// </summary>
        /// <param name="lesson">Урок</param>
        /// <returns>Созданный урок</returns>
        Task<Lesson> CreateLessonAsync(Lesson lesson);

        /// <summary>
        /// Обновить урок
        /// </summary>
        /// <param name="lesson">Урок</param>
        /// <returns>Обновленный урок</returns>
        Task<Lesson> UpdateLessonAsync(Lesson lesson);

        /// <summary>
        /// Удалить урок
        /// </summary>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Задача</returns>
        Task DeleteLessonAsync(Guid lessonId);

        /// <summary>
        /// Обновить порядок уроков
        /// </summary>
        /// <param name="lessonIds">Список идентификаторов уроков в новом порядке</param>
        /// <returns>Задача</returns>
        Task UpdateLessonsOrderAsync(IEnumerable<Guid> lessonIds);

        /// <summary>
        /// Добавить материал к уроку
        /// </summary>
        /// <param name="lessonMaterial">Материал урока</param>
        /// <returns>Добавленный материал</returns>
        Task<LessonMaterial> AddLessonMaterialAsync(LessonMaterial lessonMaterial);

        /// <summary>
        /// Удалить материал урока
        /// </summary>
        /// <param name="materialId">Идентификатор материала</param>
        /// <returns>Задача</returns>
        Task DeleteLessonMaterialAsync(Guid materialId);
    }
}