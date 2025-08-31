using System;

namespace Courses.Domain.Events
{
    /// <summary>
    /// Базовое событие прогресса по уроку
    /// </summary>
    public abstract class LessonProgressEvent
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Время создания события
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификатор прогресса
        /// </summary>
        public Guid ProgressId { get; set; }

        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }
    }

    /// <summary>
    /// Событие начала урока
    /// </summary>
    public class LessonStartedEvent : LessonProgressEvent
    {
        /// <summary>
        /// Название урока
        /// </summary>
        public string LessonTitle { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartedAt { get; set; }
    }

    /// <summary>
    /// Событие обновления прогресса по уроку
    /// </summary>
    public class LessonProgressUpdatedEvent : LessonProgressEvent
    {
        /// <summary>
        /// Название урока
        /// </summary>
        public string LessonTitle { get; set; }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public int CompletionPercentage { get; set; }
    }

    /// <summary>
    /// Событие завершения урока
    /// </summary>
    public class LessonCompletedEvent : LessonProgressEvent
    {
        /// <summary>
        /// Название урока
        /// </summary>
        public string LessonTitle { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime CompletedAt { get; set; }

        /// <summary>
        /// Время, затраченное на урок (в минутах)
        /// </summary>
        public int? TimeSpentMinutes { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public double? Score { get; set; }
    }
}