using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис отзывов
    /// </summary>
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILogService _logService;
        private readonly ILogger<ReviewService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="reviewRepository">Репозиторий отзывов</param>
        /// <param name="courseRepository">Репозиторий курсов</param>
        /// <param name="logService">Сервис логирования</param>
        /// <param name="logger">Логгер</param>
        public ReviewService(
            IReviewRepository reviewRepository,
            ICourseRepository courseRepository,
            ILogService logService,
            ILogger<ReviewService> logger)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Получить отзыв по идентификатору
        /// </summary>
        /// <param name="reviewId">Идентификатор отзыва</param>
        /// <returns>Отзыв</returns>
        public async Task<Review> GetReviewByIdAsync(Guid reviewId)
        {
            try
            {
                _logService.Information("Получение отзыва по идентификатору {ReviewId}", reviewId);
                return await _reviewRepository.GetByIdAsync(reviewId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении отзыва по идентификатору {ReviewId}", reviewId);
                throw;
            }
        }

        /// <summary>
        /// Получить отзывы по идентификатору курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Коллекция отзывов</returns>
        public async Task<IEnumerable<Review>> GetReviewsByCourseIdAsync(Guid courseId)
        {
            try
            {
                _logService.Information("Получение отзывов по идентификатору курса {CourseId}", courseId);
                return await _reviewRepository.GetReviewsByCourseIdAsync(courseId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении отзывов по идентификатору курса {CourseId}", courseId);
                throw;
            }
        }

        /// <summary>
        /// Получить отзывы по идентификатору студента
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Коллекция отзывов</returns>
        public async Task<IEnumerable<Review>> GetReviewsByStudentIdAsync(Guid studentId)
        {
            try
            {
                _logService.Information("Получение отзывов по идентификатору студента {StudentId}", studentId);
                return await _reviewRepository.GetReviewsByStudentIdAsync(studentId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении отзывов по идентификатору студента {StudentId}", studentId);
                throw;
            }
        }

        /// <summary>
        /// Создать отзыв
        /// </summary>
        /// <param name="review">Отзыв</param>
        /// <returns>Созданный отзыв</returns>
        public async Task<Review> CreateReviewAsync(Review review)
        {
            try
            {
                _logService.Information("Создание отзыва для курса {CourseId} от студента {StudentId}", review.CourseId, review.StudentId);

                // Проверка существования курса
                var course = await _courseRepository.GetByIdAsync(review.CourseId);
                if (course == null)
                {
                    _logService.Warning("Курс с идентификатором {CourseId} не найден", review.CourseId);
                    throw new ArgumentException($"Курс с идентификатором {review.CourseId} не найден");
                }

                // Установка даты создания и статуса
                review.CreatedAt = DateTime.UtcNow;
                review.IsApproved = false; // По умолчанию отзыв не одобрен

                // Сохранение отзыва
                var createdReview = await _reviewRepository.AddAsync(review);

                // Обновление рейтинга курса
                await UpdateCourseRatingAsync(review.CourseId);

                return createdReview;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при создании отзыва для курса {CourseId} от студента {StudentId}", review.CourseId, review.StudentId);
                throw;
            }
        }

        /// <summary>
        /// Обновить отзыв
        /// </summary>
        /// <param name="review">Отзыв</param>
        /// <returns>Обновленный отзыв</returns>
        public async Task<Review> UpdateReviewAsync(Review review)
        {
            try
            {
                _logService.Information("Обновление отзыва {ReviewId}", review.Id);

                // Проверка существования отзыва
                var existingReview = await _reviewRepository.GetByIdAsync(review.Id);
                if (existingReview == null)
                {
                    _logService.Warning("Отзыв с идентификатором {ReviewId} не найден", review.Id);
                    throw new ArgumentException($"Отзыв с идентификатором {review.Id} не найден");
                }

                // Обновление полей отзыва
                existingReview.Rating = review.Rating;
                existingReview.Comment = review.Comment;
                existingReview.UpdatedAt = DateTime.UtcNow;
                existingReview.IsApproved = false; // При обновлении отзыв снова требует одобрения

                // Сохранение отзыва
                var updatedReview = await _reviewRepository.UpdateAsync(existingReview);

                // Обновление рейтинга курса
                await UpdateCourseRatingAsync(updatedReview.CourseId);

                return updatedReview;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при обновлении отзыва {ReviewId}", review.Id);
                throw;
            }
        }

        /// <summary>
        /// Удалить отзыв
        /// </summary>
        /// <param name="reviewId">Идентификатор отзыва</param>
        /// <returns>Задача</returns>
        public async Task DeleteReviewAsync(Guid reviewId)
        {
            try
            {
                _logService.Information("Удаление отзыва {ReviewId}", reviewId);

                // Проверка существования отзыва
                var review = await _reviewRepository.GetByIdAsync(reviewId);
                if (review == null)
                {
                    _logService.Warning("Отзыв с идентификатором {ReviewId} не найден", reviewId);
                    throw new ArgumentException($"Отзыв с идентификатором {reviewId} не найден");
                }

                // Сохранение идентификатора курса для обновления рейтинга
                var courseId = review.CourseId;

                // Удаление отзыва
                await _reviewRepository.DeleteAsync(reviewId);

                // Обновление рейтинга курса
                await UpdateCourseRatingAsync(courseId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при удалении отзыва {ReviewId}", reviewId);
                throw;
            }
        }

        /// <summary>
        /// Одобрить отзыв
        /// </summary>
        /// <param name="reviewId">Идентификатор отзыва</param>
        /// <returns>Обновленный отзыв</returns>
        public async Task<Review> ApproveReviewAsync(Guid reviewId)
        {
            try
            {
                _logService.Information("Одобрение отзыва {ReviewId}", reviewId);

                // Проверка существования отзыва
                var review = await _reviewRepository.GetByIdAsync(reviewId);
                if (review == null)
                {
                    _logService.Warning("Отзыв с идентификатором {ReviewId} не найден", reviewId);
                    throw new ArgumentException($"Отзыв с идентификатором {reviewId} не найден");
                }

                // Обновление статуса отзыва
                review.IsApproved = true;
                review.ApprovedAt = DateTime.UtcNow;

                // Сохранение отзыва
                var updatedReview = await _reviewRepository.UpdateAsync(review);

                // Обновление рейтинга курса
                await UpdateCourseRatingAsync(updatedReview.CourseId);

                return updatedReview;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при одобрении отзыва {ReviewId}", reviewId);
                throw;
            }
        }

        /// <summary>
        /// Получить средний рейтинг курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Средний рейтинг</returns>
        public async Task<double> GetAverageRatingByCourseIdAsync(Guid courseId)
        {
            try
            {
                _logService.Information("Получение среднего рейтинга курса {CourseId}", courseId);

                // Получение всех одобренных отзывов для курса
                var reviews = await _reviewRepository.GetReviewsByCourseIdAsync(courseId);
                var approvedReviews = reviews.Where(r => r.IsApproved).ToList();

                // Расчет среднего рейтинга
                if (approvedReviews.Any())
                {
                    return approvedReviews.Average(r => r.Rating);
                }

                return 0;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении среднего рейтинга курса {CourseId}", courseId);
                throw;
            }
        }

        /// <summary>
        /// Получить неодобренные отзывы
        /// </summary>
        /// <returns>Коллекция отзывов</returns>
        public async Task<IEnumerable<Review>> GetUnapprovedReviewsAsync()
        {
            try
            {
                _logService.Information("Получение неодобренных отзывов");
                return await _reviewRepository.GetUnapprovedReviewsAsync();
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении неодобренных отзывов");
                throw;
            }
        }

        /// <summary>
        /// Обновить рейтинг курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Задача</returns>
        private async Task UpdateCourseRatingAsync(Guid courseId)
        {
            try
            {
                // Получение курса
                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    _logService.Warning("Курс с идентификатором {CourseId} не найден", courseId);
                    return;
                }

                // Получение всех одобренных отзывов для курса
                var reviews = await _reviewRepository.GetReviewsByCourseIdAsync(courseId);
                var approvedReviews = reviews.Where(r => r.IsApproved).ToList();

                // Обновление рейтинга курса
                if (approvedReviews.Any())
                {
                    course.AverageRating = approvedReviews.Average(r => r.Rating);
                    course.ReviewCount = approvedReviews.Count;
                }
                else
                {
                    course.AverageRating = 0;
                    course.ReviewCount = 0;
                }

                // Сохранение курса
                await _courseRepository.UpdateAsync(course);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при обновлении рейтинга курса {CourseId}", courseId);
                throw;
            }
        }
    }
}