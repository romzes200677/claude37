using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с уроками
    /// </summary>
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(CoursesDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить урок с материалами
        /// </summary>
        public async Task<Lesson> GetLessonWithMaterialsAsync(Guid lessonId)
        {
            return await _dbSet
                .Include(l => l.Module)
                    .ThenInclude(m => m.Course)
                .Include(l => l.Materials)
                .Include(l => l.LessonProgresses)
                .FirstOrDefaultAsync(l => l.Id == lessonId);
        }

        /// <summary>
        /// Получить уроки по идентификатору модуля
        /// </summary>
        public async Task<IEnumerable<Lesson>> GetLessonsByModuleIdAsync(Guid moduleId)
        {
            return await _dbSet
                .Where(l => l.ModuleId == moduleId)
                .OrderBy(l => l.OrderIndex)
                .ToListAsync();
        }

        /// <summary>
        /// Получить уроки по типу
        /// </summary>
        public async Task<IEnumerable<Lesson>> GetLessonsByTypeAsync(string lessonType)
        {
            return await _dbSet
                .Where(l => l.LessonType == lessonType)
                .ToListAsync();
        }
        /// <summary>
        /// Обновить порядок уроков
        /// </summary>
        public async Task UpdateLessonsOrderAsync(IEnumerable<Guid> lessonIds)
        {
            var lessons = await _dbSet
                .Where(l => lessonIds.Contains(l.Id))
                .ToListAsync();

            var lessonIdsList = lessonIds.ToList();
            for (int i = 0; i < lessonIdsList.Count; i++)
            {
                var lesson = lessons.FirstOrDefault(l => l.Id == lessonIdsList[i]);
                if (lesson != null)
                {
                    lesson.OrderIndex = i;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}