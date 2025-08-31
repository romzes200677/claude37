using System;

namespace Common.Models
{
    /// <summary>
    /// Базовый класс для всех сущностей в системе
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания сущности
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата последнего обновления сущности
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}