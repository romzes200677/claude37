using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Application.Services
{
    public interface ITestQuestionOptionResponseService
    {
        Task<TestQuestionOptionResponse> GetByIdAsync(Guid id);
        Task<IEnumerable<TestQuestionOptionResponse>> GetByQuestionResponseIdAsync(Guid questionResponseId);
        Task<TestQuestionOptionResponse> CreateAsync(TestQuestionOptionResponse optionResponse);
        Task<IEnumerable<TestQuestionOptionResponse>> CreateRangeAsync(IEnumerable<TestQuestionOptionResponse> optionResponses);
        Task<TestQuestionOptionResponse> UpdateAsync(TestQuestionOptionResponse optionResponse);
        Task DeleteAsync(Guid id);
    }
}