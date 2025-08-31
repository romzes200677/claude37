using System;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса генерации сертификатов
    /// </summary>
    public interface ICertificateService
    {
        /// <summary>
        /// Сгенерировать сертификат
        /// </summary>
        /// <param name="enrollmentId">Идентификатор зачисления</param>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="studentName">Имя студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="courseTitle">Название курса</param>
        /// <param name="completionDate">Дата завершения</param>
        /// <param name="grade">Оценка</param>
        /// <returns>URL сертификата</returns>
        Task<string> GenerateCertificateAsync(
            Guid enrollmentId,
            Guid studentId,
            string studentName,
            Guid courseId,
            string courseTitle,
            DateTime completionDate,
            decimal grade);

        /// <summary>
        /// Проверить сертификат
        /// </summary>
        /// <param name="certificateId">Идентификатор сертификата</param>
        /// <returns>Результат проверки</returns>
        Task<bool> VerifyCertificateAsync(Guid certificateId);

        /// <summary>
        /// Получить сертификат
        /// </summary>
        /// <param name="certificateId">Идентификатор сертификата</param>
        /// <returns>URL сертификата</returns>
        Task<string> GetCertificateUrlAsync(Guid certificateId);
    }
}