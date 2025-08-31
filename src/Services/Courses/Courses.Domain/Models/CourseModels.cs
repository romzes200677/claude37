using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель создания курса
    /// </summary>
    public class CreateCourseModel
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
        /// Максимальное количество студентов
        /// </summary>
        public int? MaxStudents { get; set; }

        /// <summary>
        /// Изображение курса (URL)
        /// </summary>
        public string ImageUrl { get; set; }
    }

    /// <summary>
    /// Модель обновления курса
    /// </summary>
    public class UpdateCourseModel
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid Id { get; set; }

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
        /// Максимальное количество студентов
        /// </summary>
        public int? MaxStudents { get; set; }

        /// <summary>
        /// Изображение курса (URL)
        /// </summary>
        public string ImageUrl { get; set; }
    }

    /// <summary>
    /// Модель представления курса
    /// </summary>
    public class CourseViewModel
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid Id { get; set; }

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
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Средний рейтинг
        /// </summary>
        public double? AverageRating { get; set; }

        /// <summary>
        /// Количество отзывов
        /// </summary>
        public int ReviewsCount { get; set; }

        /// <summary>
        /// Количество студентов
        /// </summary>
        public int StudentsCount { get; set; }
    }

    /// <summary>
    /// Модель детального представления курса
    /// </summary>
    public class CourseDetailViewModel : CourseViewModel
    {
        /// <summary>
        /// Модули курса
        /// </summary>
        public IEnumerable<ModuleViewModel> Modules { get; set; }

        /// <summary>
        /// Отзывы о курсе
        /// </summary>
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
    }
}