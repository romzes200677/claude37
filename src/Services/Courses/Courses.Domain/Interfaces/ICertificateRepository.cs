using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория сертификатов
    /// </summary>
    public interface ICertificateRepository : IRepository<Certificate>
    {
        /// <summary>
        /// Получить сертификаты по идентификатору студента
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Коллекция сертификатов</returns>
        Task<IEnumerable<Certificate>> GetCertificatesByStudentIdAsync(Guid studentId);

        /// <summary>
        /// Получить сертификаты по идентификатору курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Коллекция сертификатов</returns>
        Task<IEnumerable<Certificate>> GetCertificatesByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Получить сертификат по идентификатору зачисления
        /// </summary>
        /// <param name="enrollmentId">Идентификатор зачисления</param>
        /// <returns>Сертификат</returns>
        Task<Certificate> GetCertificateByEnrollmentIdAsync(Guid enrollmentId);

        /// <summary>
        /// Получить сертификаты по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Коллекция сертификатов</returns>
        Task<IEnumerable<Certificate>> GetCertificatesByFilterAsync(CertificateFilterModel filter);

        /// <summary>
        /// Получить сертификат по коду верификации
        /// </summary>
        /// <param name="verificationCode">Код верификации</param>
        /// <returns>Сертификат</returns>
        Task<Certificate> GetCertificateByVerificationCodeAsync(string verificationCode);
    }
}