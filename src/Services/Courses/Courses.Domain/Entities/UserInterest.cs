using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Сущность интереса пользователя
    /// </summary>
    public class UserInterest : BaseEntity
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Название интереса (тег, категория и т.д.)
        /// </summary>
        public string Interest { get; set; }

        /// <summary>
        /// Дата добавления
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}