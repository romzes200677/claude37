using System;
using System.Collections.Generic;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Урок
    /// </summary>
    public class Lesson : BaseEntity
    {
        /// <summary>
        /// Название урока
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание урока
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Содержимое урока
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Порядковый номер урока в модуле
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Продолжительность в минутах
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Тип урока (видео, текст, тест и т.д.)
        /// </summary>
        public string LessonType { get; set; }

        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// Модуль
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// Материалы урока
        /// </summary>
        public virtual ICollection<LessonMaterial> Materials { get; set; }

        /// <summary>
        /// Прогресс студентов по уроку
        /// </summary>
        public virtual ICollection<LessonProgress> LessonProgresses { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Lesson()
        {
            Materials = new List<LessonMaterial>();
            LessonProgresses = new List<LessonProgress>();
        }
    }
}