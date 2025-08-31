using System;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель сертификата
    /// </summary>
    public class CertificateModel
    {
        /// <summary>
        /// Идентификатор сертификата
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор зачисления
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
        /// Оценка
        /// </summary>
        public decimal Grade { get; set; }

        /// <summary>
        /// URL сертификата
        /// </summary>
        public string CertificateUrl { get; set; }

        /// <summary>
        /// Уникальный код сертификата для проверки
        /// </summary>
        public string VerificationCode { get; set; }

        /// <summary>
        /// Статус сертификата
        /// </summary>
        public CertificateStatus Status { get; set; }
    }

    /// <summary>
    /// Статус сертификата
    /// </summary>
    public enum CertificateStatus
    {
        /// <summary>
        /// Действителен
        /// </summary>
        Valid,

        /// <summary>
        /// Отозван
        /// </summary>
        Revoked,

        /// <summary>
        /// Истек срок действия
        /// </summary>
        Expired
    }

    /// <summary>
    /// Модель запроса на генерацию сертификата
    /// </summary>
    public class CertificateGenerationRequestModel
    {
        /// <summary>
        /// Идентификатор зачисления
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
        /// Дата завершения
        /// </summary>
        public DateTime CompletionDate { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public decimal Grade { get; set; }
    }

    /// <summary>
    /// Модель результата проверки сертификата
    /// </summary>
    public class CertificateVerificationResultModel
    {
        /// <summary>
        /// Результат проверки
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Статус сертификата
        /// </summary>
        public CertificateStatus Status { get; set; }

        /// <summary>
        /// Сообщение о результате проверки
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Данные сертификата (если действителен)
        /// </summary>
        public CertificateModel Certificate { get; set; }
    }

    /// <summary>
    /// Модель запроса на проверку сертификата
    /// </summary>
    public class CertificateVerificationRequestModel
    {
        /// <summary>
        /// Идентификатор сертификата
        /// </summary>
        public Guid CertificateId { get; set; }

        /// <summary>
        /// Код верификации
        /// </summary>
        public string VerificationCode { get; set; }
    }

    /// <summary>
    /// Модель фильтра для получения сертификатов
    /// </summary>
    public class CertificateFilterModel : PaginationRequestModel
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid? StudentId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid? CourseId { get; set; }

        /// <summary>
        /// Статус сертификата
        /// </summary>
        public CertificateStatus? Status { get; set; }

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public string SearchQuery { get; set; }
    }
}