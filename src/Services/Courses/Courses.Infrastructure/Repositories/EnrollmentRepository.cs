using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с записями на курсы
    /// </summary>
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(CoursesDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить запись по идентификатору студента и курса
        /// </summary>
        public async Task<Enrollment> GetEnrollmentByStudentAndCourseIdAsync(Guid studentId, Guid courseId)
        {
            return await _dbSet
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.UserId == studentId && e.CourseId == courseId);
        }

        /// <summary>
        /// Получить записи по идентификатору студента
        /// </summary>
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(Guid studentId)
        {
            return await _dbSet
                .Include(e => e.Course)
                .Where(e => e.UserId == studentId)
                .ToListAsync();
        }

        /// <summary>
        /// Получить все записи на курс
        /// </summary>
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .Include(e => e.Course)
                .Where(e => e.CourseId == courseId)
                .ToListAsync();
        }

        /// <summary>
        /// Получить количество активных записей на курс
        /// </summary>
        public async Task<int> GetActiveEnrollmentsCountByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .CountAsync(e => e.CourseId == courseId && e.Status == "Active");
        }
        /// <summary>
        /// Получить записи по статусу
        /// </summary>
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStatusAsync(string status)
        {
            return await _dbSet
                .Include(e => e.Course)
                .Where(e => e.Status == status)
                .ToListAsync();
        }
    }
}