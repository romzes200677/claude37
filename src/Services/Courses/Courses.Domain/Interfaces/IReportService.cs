using System;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса отчетов
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Сгенерировать отчет по курсу
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="format">Формат отчета (PDF, Excel, CSV)</param>
        /// <returns>URL отчета</returns>
        Task<string> GenerateCourseReportAsync(Guid courseId, string format = "PDF");

        /// <summary>
        /// Сгенерировать отчет по студенту
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="format">Формат отчета (PDF, Excel, CSV)</param>
        /// <returns>URL отчета</returns>
        Task<string> GenerateStudentReportAsync(Guid studentId, string format = "PDF");

        /// <summary>
        /// Сгенерировать отчет по автору
        /// </summary>
        /// <param name="authorId">Идентификатор автора</param>
        /// <param name="format">Формат отчета (PDF, Excel, CSV)</param>
        /// <returns>URL отчета</returns>
        Task<string> GenerateAuthorReportAsync(Guid authorId, string format = "PDF");

        /// <summary>
        /// Сгенерировать отчет по зачислению
        /// </summary>
        /// <param name="enrollmentId">Идентификатор зачисления</param>
        /// <param name="format">Формат отчета (PDF, Excel, CSV)</param>
        /// <returns>URL отчета</returns>
        Task<string> GenerateEnrollmentReportAsync(Guid enrollmentId, string format = "PDF");

        /// <summary>
        /// Сгенерировать отчет по прогрессу студента в курсе
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="format">Формат отчета (PDF, Excel, CSV)</param>
        /// <returns>URL отчета</returns>
        Task<string> GenerateStudentCourseProgressReportAsync(Guid studentId, Guid courseId, string format = "PDF");

        /// <summary>
        /// Сгенерировать отчет по активности студентов в курсе
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <param name="format">Формат отчета (PDF, Excel, CSV)</param>
        /// <returns>URL отчета</returns>
        Task<string> GenerateStudentActivityReportAsync(Guid courseId, DateTime startDate, DateTime endDate, string format = "PDF");
    }
}