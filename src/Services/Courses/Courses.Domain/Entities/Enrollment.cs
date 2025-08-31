using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Запись студента на курс
    /// </summary>
    public class Enrollment : BaseEntity
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public virtual Course Course { get; set; }

        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// Статус записи (активна, завершена, отменена)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Процент завершения курса
        /// </summary>
        public int CompletionPercentage { get; set; }

        /// <summary>
        /// Дата завершения курса
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Итоговая оценка
        /// </summary>
        public decimal? FinalGrade { get; set; }

        /// <summary>
        /// Сертификат выдан
        /// </summary>
        public bool CertificateIssued { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Enrollment()
        {
            EnrollmentDate = DateTime.UtcNow;
            Status = "Active";
            CompletionPercentage = 0;
            CertificateIssued = false;
        }
    }
}