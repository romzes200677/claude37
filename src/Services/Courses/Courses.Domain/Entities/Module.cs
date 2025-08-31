using System;
using System.Collections.Generic;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Модуль курса
    /// </summary>
    public class Module : BaseEntity
    {
        /// <summary>
        /// Название модуля
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание модуля
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Порядковый номер модуля в курсе
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public virtual Course Course { get; set; }

        /// <summary>
        /// Уроки модуля
        /// </summary>
        public virtual ICollection<Lesson> Lessons { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Module()
        {
            Lessons = new List<Lesson>();
        }
    }
}