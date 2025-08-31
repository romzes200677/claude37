using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с курсами
    /// </summary>
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(CoursesDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить курс с модулями и уроками
        /// </summary>
        public async Task<Course> GetCourseWithModulesAndLessonsAsync(Guid courseId)
        {
            return await _dbSet
                .Include(c => c.Modules)
                    .ThenInclude(m => m.Lessons)
                        .ThenInclude(l => l.Materials)
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        /// <summary>
        /// Получить опубликованные курсы
        /// </summary>
        public async Task<IEnumerable<Course>> GetPublishedCoursesAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive && c.IsPublished)
                .ToListAsync();
        }

        /// <summary>
        /// Получить курсы по идентификатору автора
        /// </summary>
        public async Task<IEnumerable<Course>> GetCoursesByAuthorIdAsync(Guid authorId)
        {
            return await _dbSet
                .Where(c => c.AuthorId == authorId)
                .ToListAsync();
        }

        /// <summary>
        /// Получить курсы по уровню сложности
        /// </summary>
        public async Task<IEnumerable<Course>> GetCoursesByDifficultyLevelAsync(string difficultyLevel)
        {
            return await _dbSet
                .Where(c => c.DifficultyLevel == difficultyLevel && c.IsActive && c.IsPublished)
                .ToListAsync();
        }

        /// <summary>
        /// Поиск курсов по названию или описанию
        /// </summary>
        public async Task<IEnumerable<Course>> SearchCoursesAsync(string searchTerm)
        {
            return await _dbSet
                .Where(c => (c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm)) && 
                       c.IsActive && c.IsPublished)
                .ToListAsync();
        }
        /// <summary>
        /// Получить курсы, на которые записан студент
        /// </summary>
        public async Task<IEnumerable<Course>> GetEnrolledCoursesAsync(Guid studentId)
        {
            return await _dbSet
                .Include(c => c.Enrollments)
                .Where(c => c.Enrollments.Any(e => e.UserId == studentId))
                .ToListAsync();
        }
    }
}