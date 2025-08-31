using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с курсами
    /// </summary>
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="courseRepository">Репозиторий курсов</param>
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// Получить все курсы
        /// </summary>
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        /// <summary>
        /// Получить опубликованные курсы
        /// </summary>
        public async Task<IEnumerable<Course>> GetPublishedCoursesAsync()
        {
            return await _courseRepository.GetPublishedCoursesAsync();
        }

        /// <summary>
        /// Получить курс по идентификатору
        /// </summary>
        public async Task<Course> GetCourseByIdAsync(Guid courseId)
        {
            return await _courseRepository.GetByIdAsync(courseId);
        }

        /// <summary>
        /// Получить курс с модулями и уроками
        /// </summary>
        public async Task<Course> GetCourseWithModulesAndLessonsAsync(Guid courseId)
        {
            return await _courseRepository.GetCourseWithModulesAndLessonsAsync(courseId);
        }

        /// <summary>
        /// Создать курс
        /// </summary>
        public async Task<Course> CreateCourseAsync(Course course)
        {
            await _courseRepository.AddAsync(course);
            return course;
        }

        /// <summary>
        /// Обновить курс
        /// </summary>
        public async Task<Course> UpdateCourseAsync(Course course)
        {
            await _courseRepository.UpdateAsync(course);
            return course;
        }

        /// <summary>
        /// Удалить курс
        /// </summary>
        public async Task DeleteCourseAsync(Guid courseId)
        {
            await _courseRepository.DeleteAsync(courseId);
        }

        /// <summary>
        /// Опубликовать курс
        /// </summary>
        public async Task<Course> PublishCourseAsync(Guid courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                throw new ArgumentException($"Курс с идентификатором {courseId} не найден");
            }

            course.IsPublished = true;
            await _courseRepository.UpdateAsync(course);
            return course;
        }

        /// <summary>
        /// Архивировать курс
        /// </summary>
        public async Task<Course> ArchiveCourseAsync(Guid courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                throw new ArgumentException($"Курс с идентификатором {courseId} не найден");
            }

            course.IsActive = false;
            await _courseRepository.UpdateAsync(course);
            return course;
        }

        /// <summary>
        /// Получить курсы по идентификатору автора
        /// </summary>
        public async Task<IEnumerable<Course>> GetCoursesByAuthorIdAsync(Guid authorId)
        {
            return await _courseRepository.GetCoursesByAuthorIdAsync(authorId);
        }

        /// <summary>
        /// Поиск курсов
        /// </summary>
        public async Task<IEnumerable<Course>> SearchCoursesAsync(string searchTerm)
        {
            return await _courseRepository.SearchCoursesAsync(searchTerm);
        }
    }
}