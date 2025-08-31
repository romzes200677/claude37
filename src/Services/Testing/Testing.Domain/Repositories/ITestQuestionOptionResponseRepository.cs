using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Domain.Repositories
{
    public interface ITestQuestionOptionResponseRepository : IRepository<TestQuestionOptionResponse>
    {
        Task<IEnumerable<TestQuestionOptionResponse>> GetByQuestionResponseIdAsync(Guid questionResponseId);
        Task<IEnumerable<TestQuestionOptionResponse>> GetSelectedOptionsAsync(Guid questionResponseId);
    }
}