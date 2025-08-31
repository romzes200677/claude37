using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса модулей
    /// </summary>
    public interface IModuleService
    {
        /// <summary>
        /// Получить модуль по идентификатору
        /// </summary>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <returns>Модуль</returns>
        Task<Module> GetModuleByIdAsync(Guid moduleId);

        /// <summary>
        /// Получить модуль с уроками
        /// </summary>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <returns>Модуль с уроками</returns>
        Task<Module> GetModuleWithLessonsAsync(Guid moduleId);

        /// <summary>
        /// Получить модули по идентификатору курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Коллекция модулей</returns>
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Создать модуль
        /// </summary>
        /// <param name="module">Модуль</param>
        /// <returns>Созданный модуль</returns>
        Task<Module> CreateModuleAsync(Module module);

        /// <summary>
        /// Обновить модуль
        /// </summary>
        /// <param name="module">Модуль</param>
        /// <returns>Обновленный модуль</returns>
        Task<Module> UpdateModuleAsync(Module module);

        /// <summary>
        /// Удалить модуль
        /// </summary>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <returns>Задача</returns>
        Task DeleteModuleAsync(Guid moduleId);

        /// <summary>
        /// Обновить порядок модулей
        /// </summary>
        /// <param name="moduleIds">Список идентификаторов модулей в новом порядке</param>
        /// <returns>Задача</returns>
        Task UpdateModulesOrderAsync(IEnumerable<Guid> moduleIds);
    }
}