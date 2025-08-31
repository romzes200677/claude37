using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с отзывами о курсах
    /// </summary>
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(CoursesDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить отзыв по идентификатору студента и курса
        /// </summary>
        public async Task<Review> GetReviewByStudentAndCourseIdAsync(Guid studentId, Guid courseId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(r => r.UserId == studentId && r.CourseId == courseId);
        }

        /// <summary>
        /// Получить все отзывы для курса
        /// </summary>
        public async Task<IEnumerable<Review>> GetReviewsByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .Where(r => r.CourseId == courseId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Получить отзывы по идентификатору студента
        /// </summary>
        public async Task<IEnumerable<Review>> GetReviewsByStudentIdAsync(Guid studentId)
        {
            return await _dbSet
                .Where(r => r.UserId == studentId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Получить средний рейтинг курса
        /// </summary>
        public async Task<double> GetAverageRatingByCourseIdAsync(Guid courseId)
        {
            var reviews = await _dbSet
                .Where(r => r.CourseId == courseId)
                .ToListAsync();

            if (!reviews.Any())
                return 0;

            return reviews.Average(r => r.Rating);
        }
        /// <summary>
        /// Получить неодобренные отзывы
        /// </summary>
        public async Task<IEnumerable<Review>> GetUnapprovedReviewsAsync()
        {
            return await _dbSet
                .Where(r => !r.IsApproved)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }
    }
}