using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Domain.Repositories
{
    public interface ITestTagRepository : IRepository<TestTag>
    {
        Task<IEnumerable<TestTag>> GetByNameAsync(string name);
        Task<IEnumerable<TestTag>> GetByTestTemplateIdAsync(Guid testTemplateId);
        Task<IEnumerable<TestTemplate>> GetTemplatesByTagAsync(Guid tagId);
    }
}