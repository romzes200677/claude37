using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Комментарий к обратной связи
    /// </summary>
    public class FeedbackComment : BaseEntity
    {
        /// <summary>
        /// Идентификатор обратной связи
        /// </summary>
        public Guid FeedbackId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Является ли комментарий внутренним (видимым только для сотрудников)
        /// </summary>
        public bool IsInternal { get; set; }

        /// <summary>
        /// Навигационное свойство для обратной связи
        /// </summary>
        public virtual Feedback Feedback { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public FeedbackComment()
        {
            IsInternal = false;
        }
    }
}