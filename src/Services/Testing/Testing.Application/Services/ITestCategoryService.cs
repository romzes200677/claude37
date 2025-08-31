using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Application.Services
{
    public interface ITestCategoryService
    {
        Task<TestCategory> GetCategoryByIdAsync(Guid id);
        Task<IEnumerable<TestCategory>> GetAllCategoriesAsync();
        Task<IEnumerable<TestCategory>> GetRootCategoriesAsync();
        Task<IEnumerable<TestCategory>> GetChildCategoriesAsync(Guid parentCategoryId);
        Task<IEnumerable<TestCategory>> GetCategoryPathAsync(Guid categoryId);
        Task<TestCategory> CreateCategoryAsync(TestCategory category);
        Task<TestCategory> UpdateCategoryAsync(TestCategory category);
        Task DeleteCategoryAsync(Guid id);
        Task<IEnumerable<TestTemplate>> GetTemplatesByCategoryAsync(Guid categoryId, bool includeChildCategories = false);
    }
}