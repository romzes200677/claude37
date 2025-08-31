using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория модулей
    /// </summary>
    public interface IModuleRepository : IRepository<Module>
    {
        /// <summary>
        /// Получить модули по идентификатору курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Коллекция модулей</returns>
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Получить модуль с уроками
        /// </summary>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <returns>Модуль с уроками</returns>
        Task<Module> GetModuleWithLessonsAsync(Guid moduleId);

        /// <summary>
        /// Обновить порядок модулей
        /// </summary>
        /// <param name="moduleIds">Список идентификаторов модулей в новом порядке</param>
        /// <returns>Задача</returns>
        Task UpdateModulesOrderAsync(IEnumerable<Guid> moduleIds);
    }
}