using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Сущность просмотра курса
    /// </summary>
    public class CourseView : BaseEntity
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Дата и время просмотра
        /// </summary>
        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// IP-адрес
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Источник перехода
        /// </summary>
        public string ReferralSource { get; set; }

        /// <summary>
        /// Устройство
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// Навигационное свойство для курса
        /// </summary>
        public Course Course { get; set; }
    }
}