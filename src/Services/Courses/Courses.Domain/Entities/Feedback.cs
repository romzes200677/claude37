using System;
using System.Collections.Generic;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Сущность обратной связи
    /// </summary>
    public class Feedback : BaseEntity
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Тип обратной связи
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Тема
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Приоритет
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Идентификатор назначенного сотрудника
        /// </summary>
        public Guid? AssigneeId { get; set; }

        /// <summary>
        /// Ответное сообщение
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// Дата ответа
        /// </summary>
        public DateTime? ResponseDate { get; set; }

        /// <summary>
        /// Идентификатор связанной сущности
        /// </summary>
        public Guid? RelatedEntityId { get; set; }

        /// <summary>
        /// Тип связанной сущности
        /// </summary>
        public string RelatedEntityType { get; set; }

        /// <summary>
        /// URL вложения
        /// </summary>
        public string AttachmentUrl { get; set; }

        /// <summary>
        /// Оценка удовлетворенности
        /// </summary>
        public int? SatisfactionRating { get; set; }

        /// <summary>
        /// Комментарии к обратной связи
        /// </summary>
        public virtual ICollection<FeedbackComment> Comments { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Feedback()
        {
            Comments = new List<FeedbackComment>();
            Status = "New";
            Priority = "Medium";
        }
    }
}