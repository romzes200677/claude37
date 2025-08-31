using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория отзывов
    /// </summary>
    public interface IReviewRepository : IRepository<Review>
    {
        /// <summary>
        /// Получить отзывы по идентификатору курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Коллекция отзывов</returns>
        Task<IEnumerable<Review>> GetReviewsByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Получить отзывы по идентификатору студента
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Коллекция отзывов</returns>
        Task<IEnumerable<Review>> GetReviewsByStudentIdAsync(Guid studentId);

        /// <summary>
        /// Получить отзыв по идентификатору студента и курса
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Отзыв</returns>
        Task<Review> GetReviewByStudentAndCourseIdAsync(Guid studentId, Guid courseId);

        /// <summary>
        /// Получить средний рейтинг курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Средний рейтинг</returns>
        Task<double> GetAverageRatingByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Получить неодобренные отзывы
        /// </summary>
        /// <returns>Коллекция отзывов</returns>
        Task<IEnumerable<Review>> GetUnapprovedReviewsAsync();
    }
}