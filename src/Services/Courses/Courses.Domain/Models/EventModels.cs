using System;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Базовая модель события
    /// </summary>
    public abstract class EventBase
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата и время создания события
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Тип события
        /// </summary>
        public string EventType => GetType().Name;

        /// <summary>
        /// Версия события
        /// </summary>
        public int Version { get; set; } = 1;
    }

    /// <summary>
    /// Событие создания курса
    /// </summary>
    public class CourseCreatedEvent : EventBase
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Категория курса
        /// </summary>
        public string Category { get; set; }
    }

    /// <summary>
    /// Событие обновления курса
    /// </summary>
    public class CourseUpdatedEvent : EventBase
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание изменений
        /// </summary>
        public string ChangeDescription { get; set; }
    }

    /// <summary>
    /// Событие публикации курса
    /// </summary>
    public class CoursePublishedEvent : EventBase
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Дата публикации
        /// </summary>
        public DateTime PublishedAt { get; set; }
    }

    /// <summary>
    /// Событие зачисления на курс
    /// </summary>
    public class CourseEnrollmentEvent : EventBase
    {
        /// <summary>
        /// Идентификатор зачисления
        /// </summary>
        public Guid EnrollmentId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Дата зачисления
        /// </summary>
        public DateTime EnrolledAt { get; set; }
    }

    /// <summary>
    /// Событие завершения курса
    /// </summary>
    public class CourseCompletedEvent : EventBase
    {
        /// <summary>
        /// Идентификатор зачисления
        /// </summary>
        public Guid EnrollmentId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime CompletedAt { get; set; }

        /// <summary>
        /// Итоговая оценка
        /// </summary>
        public decimal? FinalGrade { get; set; }
    }

    /// <summary>
    /// Событие добавления отзыва о курсе
    /// </summary>
    public class CourseReviewAddedEvent : EventBase
    {
        /// <summary>
        /// Идентификатор отзыва
        /// </summary>
        public Guid ReviewId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Рейтинг
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Дата добавления отзыва
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}