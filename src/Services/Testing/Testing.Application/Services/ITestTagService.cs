using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Application.Services
{
    public interface ITestTagService
    {
        Task<TestTag> GetTagByIdAsync(Guid id);
        Task<IEnumerable<TestTag>> GetAllTagsAsync();
        Task<IEnumerable<TestTag>> GetTagsByNameAsync(string name);
        Task<IEnumerable<TestTag>> GetTagsByTestTemplateAsync(Guid testTemplateId);
        Task<TestTag> CreateTagAsync(TestTag tag);
        Task<TestTag> UpdateTagAsync(TestTag tag);
        Task DeleteTagAsync(Guid id);
        Task<IEnumerable<TestTemplate>> GetTemplatesByTagAsync(Guid tagId);
    }
}