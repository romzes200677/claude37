using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Courses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий сертификатов
    /// </summary>
    public class CertificateRepository : Repository<Certificate>, ICertificateRepository
    {
        public CertificateRepository(CoursesDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить сертификаты по идентификатору студента
        /// </summary>
        public async Task<IEnumerable<Certificate>> GetCertificatesByStudentIdAsync(Guid studentId)
        {
            return await _dbSet
                .Where(c => c.StudentId == studentId)
                .OrderByDescending(c => c.IssueDate)
                .ToListAsync();
        }

        /// <summary>
        /// Получить сертификаты по идентификатору курса
        /// </summary>
        public async Task<IEnumerable<Certificate>> GetCertificatesByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .Where(c => c.CourseId == courseId)
                .OrderByDescending(c => c.IssueDate)
                .ToListAsync();
        }

        /// <summary>
        /// Получить сертификат по идентификатору зачисления
        /// </summary>
        public async Task<Certificate> GetCertificateByEnrollmentIdAsync(Guid enrollmentId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.EnrollmentId == enrollmentId);
        }

        /// <summary>
        /// Получить сертификаты по фильтру
        /// </summary>
        public async Task<IEnumerable<Certificate>> GetCertificatesByFilterAsync(CertificateFilterModel filter)
        {
            var query = _dbSet.AsQueryable();

            if (filter.StudentId.HasValue)
            {
                query = query.Where(c => c.StudentId == filter.StudentId.Value);
            }

            if (filter.CourseId.HasValue)
            {
                query = query.Where(c => c.CourseId == filter.CourseId.Value);
            }

            if (filter.Status.HasValue)
            {
                string status = filter.Status.Value.ToString();
                query = query.Where(c => c.Status == status);
            }

            if (filter.StartDate.HasValue)
            {
                query = query.Where(c => c.IssueDate >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(c => c.IssueDate <= filter.EndDate.Value);
            }

            if (!string.IsNullOrEmpty(filter.SearchQuery))
            {
                string searchQuery = filter.SearchQuery.ToLower();
                query = query.Where(c => 
                    c.StudentName.ToLower().Contains(searchQuery) ||
                    c.CourseTitle.ToLower().Contains(searchQuery) ||
                    c.VerificationCode.ToLower().Contains(searchQuery));
            }

            // Применяем пагинацию
            query = query.OrderByDescending(c => c.IssueDate)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Получить сертификат по коду верификации
        /// </summary>
        public async Task<Certificate> GetCertificateByVerificationCodeAsync(string verificationCode)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.VerificationCode == verificationCode);
        }
    }
}