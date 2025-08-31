using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Application.Services
{
    public interface ITestQuestionOptionService
    {
        Task<TestQuestionOption> GetOptionByIdAsync(Guid id);
        Task<IEnumerable<TestQuestionOption>> GetOptionsByQuestionAsync(Guid questionId);
        Task<IEnumerable<TestQuestionOption>> GetCorrectOptionsForQuestionAsync(Guid questionId);
        Task<TestQuestionOption> CreateOptionAsync(TestQuestionOption option);
        Task<TestQuestionOption> UpdateOptionAsync(TestQuestionOption option);
        Task DeleteOptionAsync(Guid id);
        Task ReorderOptionsAsync(Guid questionId, Dictionary<Guid, int> optionOrderMap);
    }
}