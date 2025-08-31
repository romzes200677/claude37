using System;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель создания отзыва
    /// </summary>
    public class CreateReviewModel
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Рейтинг (от 1 до 5)
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
    }

    /// <summary>
    /// Модель обновления отзыва
    /// </summary>
    public class UpdateReviewModel
    {
        /// <summary>
        /// Идентификатор отзыва
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Рейтинг (от 1 до 5)
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
    }

    /// <summary>
    /// Модель представления отзыва
    /// </summary>
    public class ReviewViewModel
    {
        /// <summary>
        /// Идентификатор отзыва
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Имя студента
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Рейтинг (от 1 до 5)
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Дата отзыва
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// Одобрен ли отзыв
        /// </summary>
        public bool IsApproved { get; set; }
    }

    /// <summary>
    /// Модель одобрения отзыва
    /// </summary>
    public class ApproveReviewModel
    {
        /// <summary>
        /// Идентификатор отзыва
        /// </summary>
        public Guid ReviewId { get; set; }
    }
}