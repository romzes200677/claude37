using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Enums;

namespace Testing.Application.Services
{
    public interface ITestAttemptService
    {
        Task<TestAttempt> GetAttemptByIdAsync(Guid id);
        Task<IEnumerable<TestAttempt>> GetAttemptsByUserAsync(Guid userId);
        Task<IEnumerable<TestAttempt>> GetAttemptsByTestTemplateAsync(Guid testTemplateId);
        Task<IEnumerable<TestAttempt>> GetAttemptsByUserAndTestTemplateAsync(Guid userId, Guid testTemplateId);
        Task<TestAttempt> GetLatestAttemptAsync(Guid userId, Guid testTemplateId);
        Task<int> GetAttemptCountAsync(Guid userId, Guid testTemplateId);
        Task<bool> CanUserStartNewAttemptAsync(Guid userId, Guid testTemplateId);
        Task<TestAttempt> StartTestAttemptAsync(Guid userId, Guid testTemplateId);
        Task<TestAttempt> SubmitTestAttemptAsync(Guid attemptId);
        Task<TestAttempt> AbandonTestAttemptAsync(Guid attemptId);
        Task<TestAttempt> UpdateTestAttemptScoreAsync(Guid attemptId);
        Task<TestAttempt> ReviewTestAttemptAsync(Guid attemptId, Guid reviewerId, string reviewNotes);
        Task<IEnumerable<TestAttempt>> GetAttemptsByStatusAsync(TestAttemptStatus status);
        Task<IEnumerable<TestAttempt>> GetAttemptsByReviewStatusAsync(ReviewStatus reviewStatus);
    }
}