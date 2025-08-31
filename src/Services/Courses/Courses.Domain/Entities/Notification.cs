using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Уведомление
    /// </summary>
    public class Notification : BaseEntity
    {
        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Тип уведомления
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст уведомления
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Прочитано ли уведомление
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Дата прочтения
        /// </summary>
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// URL для перехода
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// Идентификатор связанной сущности
        /// </summary>
        public Guid? RelatedEntityId { get; set; }

        /// <summary>
        /// Тип связанной сущности
        /// </summary>
        public string RelatedEntityType { get; set; }

        /// <summary>
        /// Дополнительные данные (JSON)
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Приоритет уведомления
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Notification()
        {
            IsRead = false;
            Priority = "Normal";
        }
    }
}