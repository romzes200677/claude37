using System;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель создания прогресса по уроку
    /// </summary>
    public class CreateLessonProgressModel
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }
    }

    /// <summary>
    /// Модель обновления прогресса по уроку
    /// </summary>
    public class UpdateLessonProgressModel
    {
        /// <summary>
        /// Идентификатор прогресса
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Статус прогресса
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public int CompletionPercentage { get; set; }

        /// <summary>
        /// Время, затраченное на урок (в минутах)
        /// </summary>
        public int? TimeSpentMinutes { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public double? Score { get; set; }
    }

    /// <summary>
    /// Модель завершения урока
    /// </summary>
    public class CompleteLessonModel
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Время, затраченное на урок (в минутах)
        /// </summary>
        public int? TimeSpentMinutes { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public double? Score { get; set; }
    }

    /// <summary>
    /// Модель представления прогресса по уроку
    /// </summary>
    public class LessonProgressViewModel
    {
        /// <summary>
        /// Идентификатор прогресса
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Название урока
        /// </summary>
        public string LessonTitle { get; set; }

        /// <summary>
        /// Статус прогресса
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public int CompletionPercentage { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Время, затраченное на урок (в минутах)
        /// </summary>
        public int? TimeSpentMinutes { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public double? Score { get; set; }
    }
}