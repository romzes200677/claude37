using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса записей на курс
    /// </summary>
    public interface IEnrollmentService
    {
        /// <summary>
        /// Получить запись по идентификатору
        /// </summary>
        /// <param name="enrollmentId">Идентификатор записи</param>
        /// <returns>Запись</returns>
        Task<Enrollment> GetEnrollmentByIdAsync(Guid enrollmentId);

        /// <summary>
        /// Получить записи по идентификатору студента
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Коллекция записей</returns>
        Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(Guid studentId);

        /// <summary>
        /// Получить записи по идентификатору курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Коллекция записей</returns>
        Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Записать студента на курс
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Созданная запись</returns>
        Task<Enrollment> EnrollStudentAsync(Guid studentId, Guid courseId);

        /// <summary>
        /// Отменить запись студента на курс
        /// </summary>
        /// <param name="enrollmentId">Идентификатор записи</param>
        /// <returns>Обновленная запись</returns>
        Task<Enrollment> CancelEnrollmentAsync(Guid enrollmentId);

        /// <summary>
        /// Завершить запись студента на курс
        /// </summary>
        /// <param name="enrollmentId">Идентификатор записи</param>
        /// <param name="finalGrade">Итоговая оценка</param>
        /// <returns>Обновленная запись</returns>
        Task<Enrollment> CompleteEnrollmentAsync(Guid enrollmentId, decimal finalGrade);

        /// <summary>
        /// Обновить прогресс записи
        /// </summary>
        /// <param name="enrollmentId">Идентификатор записи</param>
        /// <param name="completionPercentage">Процент завершения</param>
        /// <returns>Обновленная запись</returns>
        Task<Enrollment> UpdateEnrollmentProgressAsync(Guid enrollmentId, int completionPercentage);

        /// <summary>
        /// Выдать сертификат
        /// </summary>
        /// <param name="enrollmentId">Идентификатор записи</param>
        /// <returns>Обновленная запись</returns>
        Task<Enrollment> IssueCertificateAsync(Guid enrollmentId);

        /// <summary>
        /// Проверить, записан ли студент на курс
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Результат проверки</returns>
        Task<bool> IsStudentEnrolledAsync(Guid studentId, Guid courseId);
    }
}