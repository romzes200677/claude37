using System;
using System.Collections.Generic;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Вопрос опроса
    /// </summary>
    public class SurveyQuestion : BaseEntity
    {
        /// <summary>
        /// Идентификатор опроса
        /// </summary>
        public Guid SurveyId { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public string QuestionType { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Является ли вопрос обязательным
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Дополнительные настройки (JSON)
        /// </summary>
        public string Settings { get; set; }

        /// <summary>
        /// Варианты ответов
        /// </summary>
        public virtual ICollection<SurveyQuestionOption> Options { get; set; }

        /// <summary>
        /// Ответы на вопрос
        /// </summary>
        public virtual ICollection<SurveyQuestionResponse> Responses { get; set; }

        /// <summary>
        /// Навигационное свойство для опроса
        /// </summary>
        public virtual Survey Survey { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public SurveyQuestion()
        {
            Options = new List<SurveyQuestionOption>();
            Responses = new List<SurveyQuestionResponse>();
            IsRequired = true;
            QuestionType = "SingleChoice";
        }
    }
}