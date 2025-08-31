using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Domain.Repositories
{
    public interface ITestQuestionResponseRepository : IRepository<TestQuestionResponse>
    {
        Task<IEnumerable<TestQuestionResponse>> GetByTestAttemptIdAsync(Guid testAttemptId);
        Task<TestQuestionResponse> GetByTestAttemptAndQuestionIdAsync(Guid testAttemptId, Guid questionId);
        Task<IEnumerable<TestQuestionResponse>> GetCorrectResponsesAsync(Guid testAttemptId);
        Task<IEnumerable<TestQuestionResponse>> GetIncorrectResponsesAsync(Guid testAttemptId);
    }
}