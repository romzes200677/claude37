using System;

namespace Courses.Domain.Events
{
    /// <summary>
    /// Базовое событие записи на курс
    /// </summary>
    public abstract class EnrollmentEvent
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Время создания события
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid EnrollmentId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }
    }

    /// <summary>
    /// Событие создания записи на курс
    /// </summary>
    public class StudentEnrolledEvent : EnrollmentEvent
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTime EnrollmentDate { get; set; }
    }

    /// <summary>
    /// Событие отмены записи на курс
    /// </summary>
    public class EnrollmentCancelledEvent : EnrollmentEvent
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Дата отмены
        /// </summary>
        public DateTime CancellationDate { get; set; }
    }

    /// <summary>
    /// Событие завершения курса студентом
    /// </summary>
    public class CourseCompletedEvent : EnrollmentEvent
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime CompletionDate { get; set; }

        /// <summary>
        /// Итоговая оценка
        /// </summary>
        public double? FinalGrade { get; set; }
    }

    /// <summary>
    /// Событие выдачи сертификата
    /// </summary>
    public class CertificateIssuedEvent : EnrollmentEvent
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Дата выдачи сертификата
        /// </summary>
        public DateTime IssuedDate { get; set; }

        /// <summary>
        /// Итоговая оценка
        /// </summary>
        public double? FinalGrade { get; set; }
    }
}