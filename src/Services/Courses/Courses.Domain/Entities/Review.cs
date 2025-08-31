using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Отзыв о курсе
    /// </summary>
    public class Review : BaseEntity
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
        /// Курс
        /// </summary>
        public virtual Course Course { get; set; }

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
        /// Одобрен ли отзыв модератором
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Review()
        {
            ReviewDate = DateTime.UtcNow;
            IsApproved = false;
        }
    }
}