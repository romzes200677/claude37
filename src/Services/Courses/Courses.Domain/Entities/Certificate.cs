using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Сертификат о прохождении курса
    /// </summary>
    public class Certificate : BaseEntity
    {
        /// <summary>
        /// Идентификатор зачисления на курс
        /// </summary>
        public Guid EnrollmentId { get; set; }

        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Имя студента
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Дата завершения курса
        /// </summary>
        public DateTime CompletionDate { get; set; }

        /// <summary>
        /// Дата выдачи сертификата
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Итоговая оценка
        /// </summary>
        public decimal? Grade { get; set; }

        /// <summary>
        /// URL сертификата
        /// </summary>
        public string CertificateUrl { get; set; }

        /// <summary>
        /// Код верификации
        /// </summary>
        public string VerificationCode { get; set; }

        /// <summary>
        /// Статус сертификата (Valid, Revoked, Expired)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Навигационное свойство для зачисления
        /// </summary>
        public virtual Enrollment Enrollment { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Certificate()
        {
            IssueDate = DateTime.UtcNow;
            Status = "Valid";
            VerificationCode = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
    }
}