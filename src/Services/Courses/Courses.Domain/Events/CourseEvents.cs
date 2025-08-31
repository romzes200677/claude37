using System;

namespace Courses.Domain.Events
{
    /// <summary>
    /// Базовое событие курса
    /// </summary>
    public abstract class CourseEvent
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
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }
    }

    /// <summary>
    /// Событие создания курса
    /// </summary>
    public class CourseCreatedEvent : CourseEvent
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Уровень сложности
        /// </summary>
        public string DifficultyLevel { get; set; }
    }

    /// <summary>
    /// Событие обновления курса
    /// </summary>
    public class CourseUpdatedEvent : CourseEvent
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }
    }

    /// <summary>
    /// Событие публикации курса
    /// </summary>
    public class CoursePublishedEvent : CourseEvent
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public Guid AuthorId { get; set; }
    }

    /// <summary>
    /// Событие архивации курса
    /// </summary>
    public class CourseArchivedEvent : CourseEvent
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }
    }

    /// <summary>
    /// Событие удаления курса
    /// </summary>
    public class CourseDeletedEvent : CourseEvent
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }
    }
}