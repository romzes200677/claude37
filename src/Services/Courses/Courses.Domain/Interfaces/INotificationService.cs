using System;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса уведомлений
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Отправить уведомление о записи на курс
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="courseTitle">Название курса</param>
        /// <returns>Задача</returns>
        Task SendEnrollmentNotificationAsync(Guid studentId, Guid courseId, string courseTitle);

        /// <summary>
        /// Отправить уведомление о завершении курса
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="courseTitle">Название курса</param>
        /// <returns>Задача</returns>
        Task SendCourseCompletionNotificationAsync(Guid studentId, Guid courseId, string courseTitle);

        /// <summary>
        /// Отправить уведомление о выдаче сертификата
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="courseTitle">Название курса</param>
        /// <returns>Задача</returns>
        Task SendCertificateIssuedNotificationAsync(Guid studentId, Guid courseId, string courseTitle);

        /// <summary>
        /// Отправить уведомление о публикации курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="courseTitle">Название курса</param>
        /// <returns>Задача</returns>
        Task SendCoursePublishedNotificationAsync(Guid courseId, string courseTitle);

        /// <summary>
        /// Отправить уведомление о новом отзыве
        /// </summary>
        /// <param name="authorId">Идентификатор автора курса</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="courseTitle">Название курса</param>
        /// <param name="rating">Рейтинг</param>
        /// <returns>Задача</returns>
        Task SendNewReviewNotificationAsync(Guid authorId, Guid courseId, string courseTitle, int rating);
    }
}