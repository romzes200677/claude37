using System;
using System.IO;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис генерации сертификатов
    /// </summary>
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILogService _logService;
        private readonly ILogger<CertificateService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        public CertificateService(
            ICertificateRepository certificateRepository,
            IEnrollmentRepository enrollmentRepository,
            ILogService logService,
            ILogger<CertificateService> logger)
        {
            _certificateRepository = certificateRepository ?? throw new ArgumentNullException(nameof(certificateRepository));
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Сгенерировать сертификат
        /// </summary>
        public async Task<string> GenerateCertificateAsync(
            Guid enrollmentId,
            Guid studentId,
            string studentName,
            Guid courseId,
            string courseTitle,
            DateTime completionDate,
            decimal grade)
        {
            try
            {
                _logger.LogInformation($"Генерация сертификата для студента {studentId} по курсу {courseId}");

                // Проверяем, существует ли уже сертификат для этого зачисления
                var existingCertificate = await _certificateRepository.GetCertificateByEnrollmentIdAsync(enrollmentId);
                if (existingCertificate != null)
                {
                    _logger.LogInformation($"Сертификат для зачисления {enrollmentId} уже существует");
                    return existingCertificate.CertificateUrl;
                }

                // Проверяем, существует ли зачисление
                var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
                if (enrollment == null)
                {
                    throw new ArgumentException($"Зачисление с идентификатором {enrollmentId} не найдено");
                }

                // Проверяем, завершен ли курс
                if (enrollment.Status != "Completed")
                {
                    throw new InvalidOperationException("Нельзя выдать сертификат для незавершенного курса");
                }

                // Создаем сертификат
                var certificate = new Certificate
                {
                    EnrollmentId = enrollmentId,
                    StudentId = studentId,
                    StudentName = studentName,
                    CourseId = courseId,
                    CourseTitle = courseTitle,
                    CompletionDate = completionDate,
                    Grade = grade,
                    Status = "Valid"
                };

                // Генерируем URL сертификата
                string certificateFileName = $"certificate_{certificate.Id}.pdf";
                string certificateUrl = $"/certificates/{certificateFileName}";
                certificate.CertificateUrl = certificateUrl;

                // Сохраняем сертификат в базе данных
                await _certificateRepository.AddAsync(certificate);

                // Обновляем статус зачисления
                enrollment.CertificateIssued = true;
                enrollment.CertificateIssuedDate = DateTime.UtcNow;
                await _enrollmentRepository.UpdateAsync(enrollment);

                // Логируем действие
                await _logService.LogActionAsync(
                    "Certificate",
                    "Generate",
                    $"Сертификат сгенерирован для студента {studentId} по курсу {courseId}");

                // В реальном приложении здесь был бы код для генерации PDF-файла сертификата
                // и сохранения его в файловой системе или облачном хранилище

                return certificateUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при генерации сертификата для студента {studentId} по курсу {courseId}");
                throw;
            }
        }

        /// <summary>
        /// Проверить сертификат
        /// </summary>
        public async Task<bool> VerifyCertificateAsync(Guid certificateId)
        {
            try
            {
                _logger.LogInformation($"Проверка сертификата {certificateId}");

                var certificate = await _certificateRepository.GetByIdAsync(certificateId);
                if (certificate == null)
                {
                    _logger.LogWarning($"Сертификат с идентификатором {certificateId} не найден");
                    return false;
                }

                // Проверяем статус сертификата
                bool isValid = certificate.Status == "Valid";

                // Логируем действие
                await _logService.LogActionAsync(
                    "Certificate",
                    "Verify",
                    $"Проверка сертификата {certificateId}, результат: {isValid}");

                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при проверке сертификата {certificateId}");
                throw;
            }
        }

        /// <summary>
        /// Получить URL сертификата
        /// </summary>
        public async Task<string> GetCertificateUrlAsync(Guid certificateId)
        {
            try
            {
                _logger.LogInformation($"Получение URL сертификата {certificateId}");

                var certificate = await _certificateRepository.GetByIdAsync(certificateId);
                if (certificate == null)
                {
                    throw new ArgumentException($"Сертификат с идентификатором {certificateId} не найден");
                }

                // Логируем действие
                await _logService.LogActionAsync(
                    "Certificate",
                    "GetUrl",
                    $"Получен URL сертификата {certificateId}");

                return certificate.CertificateUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении URL сертификата {certificateId}");
                throw;
            }
        }
    }
}