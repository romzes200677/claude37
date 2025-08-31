using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Enums;

namespace Testing.Domain.Repositories
{
    public interface ITestAttemptRepository : IRepository<TestAttempt>
    {
        Task<IEnumerable<TestAttempt>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<TestAttempt>> GetByTestTemplateIdAsync(Guid testTemplateId);
        Task<IEnumerable<TestAttempt>> GetByUserAndTestTemplateAsync(Guid userId, Guid testTemplateId);
        Task<IEnumerable<TestAttempt>> GetByStatusAsync(TestAttemptStatus status);
        Task<IEnumerable<TestAttempt>> GetByReviewStatusAsync(ReviewStatus reviewStatus);
        Task<int> GetAttemptCountAsync(Guid userId, Guid testTemplateId);
        Task<TestAttempt?> GetLatestAttemptAsync(Guid userId, Guid testTemplateId);
        Task<IEnumerable<TestAttempt>> GetCompletedAttemptsAsync(Guid userId, Guid testTemplateId);
        Task<IEnumerable<TestAttempt>> GetPassedAttemptsAsync(Guid userId, Guid testTemplateId);
        Task<TestAttempt> GetByIdWithResponsesAsync(Guid id);
    }
}