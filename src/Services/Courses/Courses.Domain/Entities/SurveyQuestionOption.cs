using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Вариант ответа на вопрос опроса
    /// </summary>
    public class SurveyQuestionOption : BaseEntity
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Текст варианта ответа
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Дополнительные данные (JSON)
        /// </summary>
        public string AdditionalData { get; set; }

        /// <summary>
        /// Навигационное свойство для вопроса
        /// </summary>
        public virtual SurveyQuestion Question { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public SurveyQuestionOption()
        {
        }
    }
}