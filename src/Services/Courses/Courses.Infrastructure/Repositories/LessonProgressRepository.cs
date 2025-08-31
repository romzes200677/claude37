using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с прогрессом по урокам
    /// </summary>
    public class LessonProgressRepository : Repository<LessonProgress>, ILessonProgressRepository
    {
        public LessonProgressRepository(CoursesDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить прогресс по идентификатору студента и урока
        /// </summary>
        public async Task<LessonProgress> GetProgressByStudentAndLessonIdAsync(Guid studentId, Guid lessonId)
        {
            return await _dbSet
                .Include(lp => lp.Lesson)
                .FirstOrDefaultAsync(lp => lp.UserId == studentId && lp.LessonId == lessonId);
        }

        /// <summary>
        /// Получить прогресс по идентификатору студента
        /// </summary>
        public async Task<IEnumerable<LessonProgress>> GetProgressByStudentIdAsync(Guid studentId)
        {
            return await _dbSet
                .Include(lp => lp.Lesson)
                    .ThenInclude(l => l.Module)
                        .ThenInclude(m => m.Course)
                .Where(lp => lp.UserId == studentId)
                .ToListAsync();
        }

        /// <summary>
        /// Получить прогресс по идентификатору урока
        /// </summary>
        public async Task<IEnumerable<LessonProgress>> GetProgressByLessonIdAsync(Guid lessonId)
        {
            return await _dbSet
                .Where(lp => lp.LessonId == lessonId)
                .ToListAsync();
        }

        /// <summary>
        /// Получить прогресс студента по всем урокам модуля
        /// </summary>
        public async Task<IEnumerable<LessonProgress>> GetProgressByStudentAndModuleIdAsync(Guid studentId, Guid moduleId)
        {
            return await _dbSet
                .Include(lp => lp.Lesson)
                .Where(lp => lp.UserId == studentId && lp.Lesson.ModuleId == moduleId)
                .ToListAsync();
        }

        /// <summary>
        /// Получить прогресс студента по всем урокам курса
        /// </summary>
        public async Task<IEnumerable<LessonProgress>> GetProgressByStudentAndCourseIdAsync(Guid studentId, Guid courseId)
        {
            return await _dbSet
                .Include(lp => lp.Lesson)
                    .ThenInclude(l => l.Module)
                .Where(lp => lp.UserId == studentId && lp.Lesson.Module.CourseId == courseId)
                .ToListAsync();
        }
    }
}