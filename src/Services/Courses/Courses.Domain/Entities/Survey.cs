using System;
using System.Collections.Generic;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Опрос
    /// </summary>
    public class Survey : BaseEntity
    {
        /// <summary>
        /// Название опроса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание опроса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор создателя
        /// </summary>
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Статус опроса
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Целевая аудитория (JSON-строка с критериями)
        /// </summary>
        public string TargetAudience { get; set; }

        /// <summary>
        /// Является ли опрос анонимным
        /// </summary>
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// Разрешено ли несколько ответов от одного пользователя
        /// </summary>
        public bool AllowMultipleResponses { get; set; }

        /// <summary>
        /// Связанный курс (если есть)
        /// </summary>
        public Guid? CourseId { get; set; }

        /// <summary>
        /// Вопросы опроса
        /// </summary>
        public virtual ICollection<SurveyQuestion> Questions { get; set; }

        /// <summary>
        /// Ответы на опрос
        /// </summary>
        public virtual ICollection<SurveyResponse> Responses { get; set; }

        /// <summary>
        /// Навигационное свойство для курса
        /// </summary>
        public virtual Course Course { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Survey()
        {
            Questions = new List<SurveyQuestion>();
            Responses = new List<SurveyResponse>();
            Status = "Draft";
            IsAnonymous = false;
            AllowMultipleResponses = false;
        }
    }
}