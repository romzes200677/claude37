using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с модулями
    /// </summary>
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="moduleRepository">Репозиторий модулей</param>
        /// <param name="courseRepository">Репозиторий курсов</param>
        public ModuleService(
            IModuleRepository moduleRepository,
            ICourseRepository courseRepository)
        {
            _moduleRepository = moduleRepository;
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// Получить модуль по идентификатору
        /// </summary>
        public async Task<Module> GetModuleByIdAsync(Guid moduleId)
        {
            return await _moduleRepository.GetByIdAsync(moduleId);
        }

        /// <summary>
        /// Получить модуль с уроками
        /// </summary>
        public async Task<Module> GetModuleWithLessonsAsync(Guid moduleId)
        {
            return await _moduleRepository.GetModuleWithLessonsAsync(moduleId);
        }

        /// <summary>
        /// Получить модули по идентификатору курса
        /// </summary>
        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(Guid courseId)
        {
            return await _moduleRepository.GetModulesByCourseIdAsync(courseId);
        }

        /// <summary>
        /// Создать модуль
        /// </summary>
        public async Task<Module> CreateModuleAsync(Module module)
        {
            // Проверяем, существует ли курс
            var course = await _courseRepository.GetByIdAsync(module.CourseId);
            if (course == null)
            {
                throw new ArgumentException($"Курс с идентификатором {module.CourseId} не найден");
            }

            // Получаем все модули курса для определения порядкового номера
            var modules = await _moduleRepository.GetModulesByCourseIdAsync(module.CourseId);
            module.OrderIndex = modules.Count();

            await _moduleRepository.AddAsync(module);
            return module;
        }

        /// <summary>
        /// Обновить модуль
        /// </summary>
        public async Task<Module> UpdateModuleAsync(Module module)
        {
            await _moduleRepository.UpdateAsync(module);
            return module;
        }

        /// <summary>
        /// Удалить модуль
        /// </summary>
        public async Task DeleteModuleAsync(Guid moduleId)
        {
            await _moduleRepository.DeleteAsync(moduleId);
        }

        /// <summary>
        /// Обновить порядок модулей
        /// </summary>
        public async Task UpdateModulesOrderAsync(IEnumerable<Guid> moduleIds)
        {
            await _moduleRepository.UpdateModulesOrderAsync(moduleIds);
        }
    }
}