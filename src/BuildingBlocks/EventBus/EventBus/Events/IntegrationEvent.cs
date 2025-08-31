using System;

namespace EventBus.Events
{
    /// <summary>
    /// Базовый класс для интеграционных событий
    /// </summary>
    public abstract class IntegrationEvent
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Время создания события
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        /// <param name="creationDate">Время создания события</param>
        protected IntegrationEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }
    }
}