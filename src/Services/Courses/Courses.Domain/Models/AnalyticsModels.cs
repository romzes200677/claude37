using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель статистики по курсу
    /// </summary>
    public class CourseStatisticsModel
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Общее количество зачислений
        /// </summary>
        public int TotalEnrollments { get; set; }

        /// <summary>
        /// Количество активных зачислений
        /// </summary>
        public int ActiveEnrollments { get; set; }

        /// <summary>
        /// Количество завершенных зачислений
        /// </summary>
        public int CompletedEnrollments { get; set; }

        /// <summary>
        /// Средний рейтинг
        /// </summary>
        public decimal AverageRating { get; set; }

        /// <summary>
        /// Количество отзывов
        /// </summary>
        public int ReviewCount { get; set; }

        /// <summary>
        /// Средний процент завершения
        /// </summary>
        public decimal AverageCompletionPercentage { get; set; }

        /// <summary>
        /// Среднее время завершения (в днях)
        /// </summary>
        public decimal AverageCompletionTime { get; set; }

        /// <summary>
        /// Статистика по модулям
        /// </summary>
        public List<ModuleStatisticsModel> ModuleStatistics { get; set; }
    }

    /// <summary>
    /// Модель статистики по модулю
    /// </summary>
    public class ModuleStatisticsModel
    {
        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// Название модуля
        /// </summary>
        public string ModuleTitle { get; set; }

        /// <summary>
        /// Средний процент завершения
        /// </summary>
        public decimal AverageCompletionPercentage { get; set; }

        /// <summary>
        /// Среднее время завершения (в часах)
        /// </summary>
        public decimal AverageCompletionTime { get; set; }

        /// <summary>
        /// Статистика по урокам
        /// </summary>
        public List<LessonStatisticsModel> LessonStatistics { get; set; }
    }

    /// <summary>
    /// Модель статистики по уроку
    /// </summary>
    public class LessonStatisticsModel
    {
        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Название урока
        /// </summary>
        public string LessonTitle { get; set; }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public decimal CompletionPercentage { get; set; }

        /// <summary>
        /// Среднее время завершения (в минутах)
        /// </summary>
        public decimal AverageCompletionTime { get; set; }

        /// <summary>
        /// Средний балл
        /// </summary>
        public decimal AverageScore { get; set; }
    }

    /// <summary>
    /// Модель статистики по студенту
    /// </summary>
    public class StudentStatisticsModel
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Имя студента
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// Общее количество зачислений
        /// </summary>
        public int TotalEnrollments { get; set; }

        /// <summary>
        /// Количество активных зачислений
        /// </summary>
        public int ActiveEnrollments { get; set; }

        /// <summary>
        /// Количество завершенных зачислений
        /// </summary>
        public int CompletedEnrollments { get; set; }

        /// <summary>
        /// Средний процент завершения
        /// </summary>
        public decimal AverageCompletionPercentage { get; set; }

        /// <summary>
        /// Средний балл
        /// </summary>
        public decimal AverageGrade { get; set; }

        /// <summary>
        /// Количество полученных сертификатов
        /// </summary>
        public int CertificatesEarned { get; set; }

        /// <summary>
        /// Статистика по курсам
        /// </summary>
        public List<StudentCourseStatisticsModel> CourseStatistics { get; set; }
    }

    /// <summary>
    /// Модель статистики по курсу студента
    /// </summary>
    public class StudentCourseStatisticsModel
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Статус зачисления
        /// </summary>
        public string EnrollmentStatus { get; set; }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public decimal CompletionPercentage { get; set; }

        /// <summary>
        /// Дата зачисления
        /// </summary>
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Итоговая оценка
        /// </summary>
        public decimal? FinalGrade { get; set; }
    }

    /// <summary>
    /// Модель статистики по автору
    /// </summary>
    public class AuthorStatisticsModel
    {
        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Общее количество курсов
        /// </summary>
        public int TotalCourses { get; set; }

        /// <summary>
        /// Количество опубликованных курсов
        /// </summary>
        public int PublishedCourses { get; set; }

        /// <summary>
        /// Количество архивированных курсов
        /// </summary>
        public int ArchivedCourses { get; set; }

        /// <summary>
        /// Общее количество зачислений
        /// </summary>
        public int TotalEnrollments { get; set; }

        /// <summary>
        /// Количество активных зачислений
        /// </summary>
        public int ActiveEnrollments { get; set; }

        /// <summary>
        /// Количество завершенных зачислений
        /// </summary>
        public int CompletedEnrollments { get; set; }

        /// <summary>
        /// Средний рейтинг
        /// </summary>
        public decimal AverageRating { get; set; }

        /// <summary>
        /// Общее количество отзывов
        /// </summary>
        public int TotalReviews { get; set; }

        /// <summary>
        /// Статистика по курсам
        /// </summary>
        public List<AuthorCourseStatisticsModel> CourseStatistics { get; set; }
    }

    /// <summary>
    /// Модель статистики по курсу автора
    /// </summary>
    public class AuthorCourseStatisticsModel
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Статус курса
        /// </summary>
        public string CourseStatus { get; set; }

        /// <summary>
        /// Количество зачислений
        /// </summary>
        public int EnrollmentCount { get; set; }

        /// <summary>
        /// Средний рейтинг
        /// </summary>
        public decimal AverageRating { get; set; }

        /// <summary>
        /// Количество отзывов
        /// </summary>
        public int ReviewCount { get; set; }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public decimal CompletionPercentage { get; set; }
    }

    /// <summary>
    /// Модель популярного курса
    /// </summary>
    public class PopularCourseModel
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Количество зачислений
        /// </summary>
        public int EnrollmentCount { get; set; }

        /// <summary>
        /// Средний рейтинг
        /// </summary>
        public decimal AverageRating { get; set; }

        /// <summary>
        /// Количество отзывов
        /// </summary>
        public int ReviewCount { get; set; }
    }

    /// <summary>
    /// Модель статистики по завершению курса
    /// </summary>
    public class CourseCompletionStatisticsModel
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Процент завершения по дням
        /// </summary>
        public Dictionary<DateTime, decimal> CompletionPercentageByDay { get; set; }

        /// <summary>
        /// Количество завершений по дням
        /// </summary>
        public Dictionary<DateTime, int> CompletionCountByDay { get; set; }

        /// <summary>
        /// Среднее время завершения по модулям (в часах)
        /// </summary>
        public Dictionary<string, decimal> AverageCompletionTimeByModule { get; set; }

        /// <summary>
        /// Распределение оценок
        /// </summary>
        public Dictionary<string, int> GradeDistribution { get; set; }
    }
}