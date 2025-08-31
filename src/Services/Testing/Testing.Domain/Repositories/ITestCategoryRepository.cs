using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Domain.Repositories
{
    public interface ITestCategoryRepository : IRepository<TestCategory>
    {
        Task<IEnumerable<TestCategory>> GetRootCategoriesAsync();
        Task<IEnumerable<TestCategory>> GetChildCategoriesAsync(Guid parentCategoryId);
        Task<IEnumerable<TestCategory>> GetCategoryPathAsync(Guid categoryId);
        Task<IEnumerable<TestTemplate>> GetTemplatesByCategoryAsync(Guid categoryId, bool includeChildCategories = false);
    }
}