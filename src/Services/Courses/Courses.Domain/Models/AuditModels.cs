using System;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель записи аудита
    /// </summary>
    public class AuditLogModel
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Действие
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public Guid EntityId { get; set; }

        /// <summary>
        /// Детали
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// IP-адрес
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Дата и время
        /// </summary>
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Модель фильтра записей аудита
    /// </summary>
    public class AuditLogFilterModel : PaginationRequestModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Действие
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public Guid? EntityId { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// IP-адрес
        /// </summary>
        public string IpAddress { get; set; }
    }
}