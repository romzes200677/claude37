using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса отзывов
    /// </summary>
    public interface IReviewService
    {
        /// <summary>
        /// Получить отзыв по идентификатору
        /// </summary>
        /// <param name="reviewId">Идентификатор отзыва</param>
        /// <returns>Отзыв</returns>
        Task<Review> GetReviewByIdAsync(Guid reviewId);

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
        /// Создать отзыв
        /// </summary>
        /// <param name="review">Отзыв</param>
        /// <returns>Созданный отзыв</returns>
        Task<Review> CreateReviewAsync(Review review);

        /// <summary>
        /// Обновить отзыв
        /// </summary>
        /// <param name="review">Отзыв</param>
        /// <returns>Обновленный отзыв</returns>
        Task<Review> UpdateReviewAsync(Review review);

        /// <summary>
        /// Удалить отзыв
        /// </summary>
        /// <param name="reviewId">Идентификатор отзыва</param>
        /// <returns>Задача</returns>
        Task DeleteReviewAsync(Guid reviewId);

        /// <summary>
        /// Одобрить отзыв
        /// </summary>
        /// <param name="reviewId">Идентификатор отзыва</param>
        /// <returns>Обновленный отзыв</returns>
        Task<Review> ApproveReviewAsync(Guid reviewId);

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