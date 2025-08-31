using System;
using System.Collections.Generic;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Курс
    /// </summary>
    public class Course : BaseEntity
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание курса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Уровень сложности
        /// </summary>
        public string DifficultyLevel { get; set; }

        /// <summary>
        /// Продолжительность в часах
        /// </summary>
        public int DurationHours { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Идентификатор автора (преподавателя)
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Максимальное количество студентов
        /// </summary>
        public int? MaxStudents { get; set; }

        /// <summary>
        /// Активен ли курс
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Опубликован ли курс
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Изображение курса (URL)
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Модули курса
        /// </summary>
        public virtual ICollection<Module> Modules { get; set; }

        /// <summary>
        /// Записи студентов на курс
        /// </summary>
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Course()
        {
            Modules = new List<Module>();
            Enrollments = new List<Enrollment>();
            IsActive = true;
            IsPublished = false;
        }
    }
}