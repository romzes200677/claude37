using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Ответ на вопрос опроса
    /// </summary>
    public class SurveyQuestionResponse : BaseEntity
    {
        /// <summary>
        /// Идентификатор ответа на опрос
        /// </summary>
        public Guid ResponseId { get; set; }

        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Идентификатор выбранного варианта ответа (для вопросов с выбором)
        /// </summary>
        public Guid? SelectedOptionId { get; set; }

        /// <summary>
        /// Текстовый ответ (для открытых вопросов)
        /// </summary>
        public string TextAnswer { get; set; }

        /// <summary>
        /// Числовой ответ (для рейтингов и шкал)
        /// </summary>
        public decimal? NumericAnswer { get; set; }

        /// <summary>
        /// Навигационное свойство для ответа на опрос
        /// </summary>
        public virtual SurveyResponse Response { get; set; }

        /// <summary>
        /// Навигационное свойство для вопроса
        /// </summary>
        public virtual SurveyQuestion Question { get; set; }

        /// <summary>
        /// Навигационное свойство для выбранного варианта ответа
        /// </summary>
        public virtual SurveyQuestionOption SelectedOption { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public SurveyQuestionResponse()
        {
        }
    }
}