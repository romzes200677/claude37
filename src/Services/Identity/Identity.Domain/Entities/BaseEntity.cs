using System;

namespace Identity.Domain.Entities
{
    /// <summary>
    /// Базовый класс для всех сущностей
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}