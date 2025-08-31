using System;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель создания записи на курс
    /// </summary>
    public class CreateEnrollmentModel
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }
    }

    /// <summary>
    /// Модель обновления статуса записи на курс
    /// </summary>
    public class UpdateEnrollmentStatusModel
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// Модель обновления прогресса записи на курс
    /// </summary>
    public class UpdateEnrollmentProgressModel
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public int CompletionPercentage { get; set; }
    }

    /// <summary>
    /// Модель представления записи на курс
    /// </summary>
    public class EnrollmentViewModel
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Процент завершения
        /// </summary>
        public int CompletionPercentage { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Итоговая оценка
        /// </summary>
        public double? FinalGrade { get; set; }

        /// <summary>
        /// Выдан ли сертификат
        /// </summary>
        public bool CertificateIssued { get; set; }
    }

    /// <summary>
    /// Модель выдачи сертификата
    /// </summary>
    public class IssueCertificateModel
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid EnrollmentId { get; set; }
    }
}