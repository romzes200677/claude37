using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с записями на курс
    /// </summary>
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="enrollmentRepository">Репозиторий записей</param>
        /// <param name="courseRepository">Репозиторий курсов</param>
        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// Получить запись по идентификатору
        /// </summary>
        public async Task<Enrollment> GetEnrollmentByIdAsync(Guid enrollmentId)
        {
            return await _enrollmentRepository.GetByIdAsync(enrollmentId);
        }

        /// <summary>
        /// Получить записи по идентификатору студента
        /// </summary>
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(Guid studentId)
        {
            return await _enrollmentRepository.GetEnrollmentsByStudentIdAsync(studentId);
        }

        /// <summary>
        /// Получить записи по идентификатору курса
        /// </summary>
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(Guid courseId)
        {
            return await _enrollmentRepository.GetEnrollmentsByCourseIdAsync(courseId);
        }

        /// <summary>
        /// Записать студента на курс
        /// </summary>
        public async Task<Enrollment> EnrollStudentAsync(Guid studentId, Guid courseId)
        {
            // Проверяем, существует ли курс
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                throw new ArgumentException($"Курс с идентификатором {courseId} не найден");
            }

            // Проверяем, не записан ли уже студент на этот курс
            var existingEnrollment = await _enrollmentRepository.GetEnrollmentByStudentAndCourseIdAsync(studentId, courseId);
            if (existingEnrollment != null)
            {
                throw new InvalidOperationException("Студент уже записан на этот курс");
            }

            // Проверяем, не превышено ли максимальное количество студентов
            if (course.MaxStudents.HasValue)
            {
                var currentEnrollmentsCount = await _enrollmentRepository.GetActiveEnrollmentsCountByCourseIdAsync(courseId);
                if (currentEnrollmentsCount >= course.MaxStudents.Value)
                {
                    throw new InvalidOperationException("Превышено максимальное количество студентов на курсе");
                }
            }

            // Создаем новую запись
            var enrollment = new Enrollment
            {
                UserId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.UtcNow,
                Status = "Active",
                CompletionPercentage = 0
            };

            await _enrollmentRepository.AddAsync(enrollment);
            return enrollment;
        }

        /// <summary>
        /// Отменить запись студента на курс
        /// </summary>
        public async Task<Enrollment> CancelEnrollmentAsync(Guid enrollmentId)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
            if (enrollment == null)
            {
                throw new ArgumentException($"Запись с идентификатором {enrollmentId} не найдена");
            }

            enrollment.Status = "Cancelled";
            enrollment.CancellationDate = DateTime.UtcNow;

            await _enrollmentRepository.UpdateAsync(enrollment);
            return enrollment;
        }

        /// <summary>
        /// Завершить запись студента на курс
        /// </summary>
        public async Task<Enrollment> CompleteEnrollmentAsync(Guid enrollmentId, decimal finalGrade)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
            if (enrollment == null)
            {
                throw new ArgumentException($"Запись с идентификатором {enrollmentId} не найдена");
            }

            enrollment.Status = "Completed";
            enrollment.CompletionDate = DateTime.UtcNow;
            enrollment.FinalGrade = finalGrade;
            enrollment.CompletionPercentage = 100;

            await _enrollmentRepository.UpdateAsync(enrollment);
            return enrollment;
        }

        /// <summary>
        /// Обновить прогресс записи
        /// </summary>
        public async Task<Enrollment> UpdateEnrollmentProgressAsync(Guid enrollmentId, int completionPercentage)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
            if (enrollment == null)
            {
                throw new ArgumentException($"Запись с идентификатором {enrollmentId} не найдена");
            }

            enrollment.CompletionPercentage = completionPercentage;
            if (completionPercentage >= 100)
            {
                enrollment.CompletionPercentage = 100;
            }

            await _enrollmentRepository.UpdateAsync(enrollment);
            return enrollment;
        }

        /// <summary>
        /// Выдать сертификат
        /// </summary>
        public async Task<Enrollment> IssueCertificateAsync(Guid enrollmentId)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
            if (enrollment == null)
            {
                throw new ArgumentException($"Запись с идентификатором {enrollmentId} не найдена");
            }

            if (enrollment.Status != "Completed")
            {
                throw new InvalidOperationException("Нельзя выдать сертификат для незавершенного курса");
            }

            enrollment.CertificateIssued = true;
            enrollment.CertificateIssuedDate = DateTime.UtcNow;

            await _enrollmentRepository.UpdateAsync(enrollment);
            return enrollment;
        }

        /// <summary>
        /// Проверить, записан ли студент на курс
        /// </summary>
        public async Task<bool> IsStudentEnrolledAsync(Guid studentId, Guid courseId)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByStudentAndCourseIdAsync(studentId, courseId);
            return enrollment != null && enrollment.Status == "Active";
        }
    }
}