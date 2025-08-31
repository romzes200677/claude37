using System;
using System.Collections.Generic;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Ответ на опрос
    /// </summary>
    public class SurveyResponse : BaseEntity
    {
        /// <summary>
        /// Идентификатор опроса
        /// </summary>
        public Guid SurveyId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Имя пользователя (для анонимных опросов)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email пользователя (для анонимных опросов)
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime CompletedAt { get; set; }

        /// <summary>
        /// IP-адрес
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Метаданные (JSON)
        /// </summary>
        public string Metadata { get; set; }

        /// <summary>
        /// Ответы на вопросы
        /// </summary>
        public virtual ICollection<SurveyQuestionResponse> QuestionResponses { get; set; }

        /// <summary>
        /// Навигационное свойство для опроса
        /// </summary>
        public virtual Survey Survey { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public SurveyResponse()
        {
            QuestionResponses = new List<SurveyQuestionResponse>();
            CompletedAt = DateTime.UtcNow;
        }
    }
}