using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Прогресс студента по уроку
    /// </summary>
    public class LessonProgress : BaseEntity
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Урок
        /// </summary>
        public virtual Lesson Lesson { get; set; }

        /// <summary>
        /// Статус прохождения (не начат, в процессе, завершен)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public int CompletionPercentage { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Время, затраченное на урок (в минутах)
        /// </summary>
        public int? TimeSpentMinutes { get; set; }

        /// <summary>
        /// Оценка (если применимо)
        /// </summary>
        public decimal? Score { get; set; }
    }
}