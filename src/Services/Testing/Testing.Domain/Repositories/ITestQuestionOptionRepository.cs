using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Domain.Repositories
{
    public interface ITestQuestionOptionRepository : IRepository<TestQuestionOption>
    {
        Task<IEnumerable<TestQuestionOption>> GetByQuestionIdAsync(Guid questionId);
        Task<IEnumerable<TestQuestionOption>> GetCorrectOptionsForQuestionAsync(Guid questionId);
        Task ReorderOptionsAsync(Guid questionId, Dictionary<Guid, int> optionOrderMap);
    }
}