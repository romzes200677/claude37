using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с модулями курса
    /// </summary>
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(CoursesDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить модуль с уроками
        /// </summary>
        public async Task<Module> GetModuleWithLessonsAsync(Guid moduleId)
        {
            return await _dbSet
                .Include(m => m.Course)
                .Include(m => m.Lessons)
                    .ThenInclude(l => l.Materials)
                .FirstOrDefaultAsync(m => m.Id == moduleId);
        }

        /// <summary>
        /// Получить модули по идентификатору курса
        /// </summary>
        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .Where(m => m.CourseId == courseId)
                .OrderBy(m => m.OrderIndex)
                .ToListAsync();
        }
        /// <summary>
        /// Обновить порядок модулей
        /// </summary>
        public async Task UpdateModulesOrderAsync(IEnumerable<Guid> moduleIds)
        {
            var modules = await _dbSet
                .Where(m => moduleIds.Contains(m.Id))
                .ToListAsync();

            var moduleIdsList = moduleIds.ToList();
            for (int i = 0; i < moduleIdsList.Count; i++)
            {
                var module = modules.FirstOrDefault(m => m.Id == moduleIdsList[i]);
                if (module != null)
                {
                    module.OrderIndex = i;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}