using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель создания урока
    /// </summary>
    public class CreateLessonModel
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
        /// Содержание урока
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Продолжительность в минутах
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Тип урока
        /// </summary>
        public string LessonType { get; set; }

        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        public Guid ModuleId { get; set; }
    }

    /// <summary>
    /// Модель обновления урока
    /// </summary>
    public class UpdateLessonModel
    {
        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название урока
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание урока
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Содержание урока
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Продолжительность в минутах
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Тип урока
        /// </summary>
        public string LessonType { get; set; }
    }

    /// <summary>
    /// Модель представления урока
    /// </summary>
    public class LessonViewModel
    {
        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название урока
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание урока
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Содержание урока
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Продолжительность в минутах
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Тип урока
        /// </summary>
        public string LessonType { get; set; }

        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Модель детального представления урока
    /// </summary>
    public class LessonDetailViewModel : LessonViewModel
    {
        /// <summary>
        /// Материалы урока
        /// </summary>
        public IEnumerable<LessonMaterialViewModel> Materials { get; set; }
    }

    /// <summary>
    /// Модель обновления порядка уроков
    /// </summary>
    public class UpdateLessonsOrderModel
    {
        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// Список идентификаторов уроков в новом порядке
        /// </summary>
        public List<Guid> LessonIds { get; set; }
    }
}