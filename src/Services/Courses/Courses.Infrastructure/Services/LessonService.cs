using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с уроками
    /// </summary>
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IModuleRepository _moduleRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="lessonRepository">Репозиторий уроков</param>
        /// <param name="moduleRepository">Репозиторий модулей</param>
        public LessonService(
            ILessonRepository lessonRepository,
            IModuleRepository moduleRepository)
        {
            _lessonRepository = lessonRepository;
            _moduleRepository = moduleRepository;
        }

        /// <summary>
        /// Получить урок по идентификатору
        /// </summary>
        public async Task<Lesson> GetLessonByIdAsync(Guid lessonId)
        {
            return await _lessonRepository.GetByIdAsync(lessonId);
        }

        /// <summary>
        /// Получить урок с материалами
        /// </summary>
        public async Task<Lesson> GetLessonWithMaterialsAsync(Guid lessonId)
        {
            return await _lessonRepository.GetLessonWithMaterialsAsync(lessonId);
        }

        /// <summary>
        /// Получить уроки по идентификатору модуля
        /// </summary>
        public async Task<IEnumerable<Lesson>> GetLessonsByModuleIdAsync(Guid moduleId)
        {
            return await _lessonRepository.GetLessonsByModuleIdAsync(moduleId);
        }

        /// <summary>
        /// Создать урок
        /// </summary>
        public async Task<Lesson> CreateLessonAsync(Lesson lesson)
        {
            // Проверяем, существует ли модуль
            var module = await _moduleRepository.GetByIdAsync(lesson.ModuleId);
            if (module == null)
            {
                throw new ArgumentException($"Модуль с идентификатором {lesson.ModuleId} не найден");
            }

            // Получаем все уроки модуля для определения порядкового номера
            var lessons = await _lessonRepository.GetLessonsByModuleIdAsync(lesson.ModuleId);
            lesson.OrderIndex = lessons.Count();

            await _lessonRepository.AddAsync(lesson);
            return lesson;
        }

        /// <summary>
        /// Обновить урок
        /// </summary>
        public async Task<Lesson> UpdateLessonAsync(Lesson lesson)
        {
            await _lessonRepository.UpdateAsync(lesson);
            return lesson;
        }

        /// <summary>
        /// Удалить урок
        /// </summary>
        public async Task DeleteLessonAsync(Guid lessonId)
        {
            await _lessonRepository.DeleteAsync(lessonId);
        }

        /// <summary>
        /// Обновить порядок уроков
        /// </summary>
        public async Task UpdateLessonsOrderAsync(IEnumerable<Guid> lessonIds)
        {
            await _lessonRepository.UpdateLessonsOrderAsync(lessonIds);
        }

        /// <summary>
        /// Добавить материал к уроку
        /// </summary>
        public async Task<LessonMaterial> AddLessonMaterialAsync(LessonMaterial lessonMaterial)
        {
            // Проверяем, существует ли урок
            var lesson = await _lessonRepository.GetByIdAsync(lessonMaterial.LessonId);
            if (lesson == null)
            {
                throw new ArgumentException($"Урок с идентификатором {lessonMaterial.LessonId} не найден");
            }

            // Получаем урок с материалами
            var lessonWithMaterials = await _lessonRepository.GetLessonWithMaterialsAsync(lessonMaterial.LessonId);
            
            // Добавляем материал к уроку
            lessonWithMaterials.Materials.Add(lessonMaterial);
            
            // Обновляем урок
            await _lessonRepository.UpdateAsync(lessonWithMaterials);
            
            return lessonMaterial;
        }

        /// <summary>
        /// Удалить материал урока
        /// </summary>
        public async Task DeleteLessonMaterialAsync(Guid materialId)
        {
            // Находим урок, содержащий материал
            var lessons = await _lessonRepository.GetAllAsync();
            foreach (var lesson in lessons)
            {
                var lessonWithMaterials = await _lessonRepository.GetLessonWithMaterialsAsync(lesson.Id);
                var material = lessonWithMaterials.Materials.FirstOrDefault(m => m.Id == materialId);
                
                if (material != null)
                {
                    // Удаляем материал из урока
                    lessonWithMaterials.Materials.Remove(material);
                    
                    // Обновляем урок
                    await _lessonRepository.UpdateAsync(lessonWithMaterials);
                    return;
                }
            }
            
            throw new ArgumentException($"Материал с идентификатором {materialId} не найден");
        }
    }
}