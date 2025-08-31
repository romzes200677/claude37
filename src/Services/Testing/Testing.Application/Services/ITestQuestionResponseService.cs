using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Application.Services
{
    public interface ITestQuestionResponseService
    {
        Task<TestQuestionResponse> GetResponseByIdAsync(Guid id);
        Task<IEnumerable<TestQuestionResponse>> GetResponsesByAttemptAsync(Guid attemptId);
        Task<TestQuestionResponse> GetResponseByAttemptAndQuestionAsync(Guid attemptId, Guid questionId);
        Task<TestQuestionResponse> SaveTextResponseAsync(Guid attemptId, Guid questionId, string responseText);
        Task<TestQuestionResponse> SaveOptionResponsesAsync(Guid attemptId, Guid questionId, IEnumerable<Guid> selectedOptionIds);
        Task<TestQuestionResponse> EvaluateResponseAsync(Guid responseId);
        Task<TestQuestionResponse> ProvideReviewerFeedbackAsync(Guid responseId, string feedback);
        Task<IEnumerable<TestQuestionResponse>> GetCorrectResponsesAsync(Guid attemptId);
        Task<IEnumerable<TestQuestionResponse>> GetIncorrectResponsesAsync(Guid attemptId);
    }
}