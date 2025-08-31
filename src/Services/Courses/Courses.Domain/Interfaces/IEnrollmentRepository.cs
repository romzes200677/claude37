using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория записей на курс
    /// </summary>
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
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
        /// Получить запись по идентификатору студента и курса
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Запись</returns>
        Task<Enrollment> GetEnrollmentByStudentAndCourseIdAsync(Guid studentId, Guid courseId);

        /// <summary>
        /// Получить количество активных записей на курс
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Количество записей</returns>
        Task<int> GetActiveEnrollmentsCountByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Получить записи по статусу
        /// </summary>
        /// <param name="status">Статус записи</param>
        /// <returns>Коллекция записей</returns>
        Task<IEnumerable<Enrollment>> GetEnrollmentsByStatusAsync(string status);
    }
}