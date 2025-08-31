using Microsoft.EntityFrameworkCore;
using Testing.Domain.Entities;
using Testing.Domain.Repositories;
using Testing.Infrastructure.Data;

namespace Testing.Infrastructure.Repositories
{
    public class TestCategoryRepository : Repository<TestCategory>, ITestCategoryRepository
    {
        public TestCategoryRepository(TestingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TestCategory>> GetRootCategoriesAsync()
        {
            return await _context.TestCategories
                .Where(c => c.ParentCategoryId == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestCategory>> GetChildCategoriesAsync(Guid parentCategoryId)
        {
            return await _context.TestCategories
                .Where(c => c.ParentCategoryId == parentCategoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestCategory>> GetCategoryPathAsync(Guid categoryId)
        {
            var result = new List<TestCategory>();
            var currentCategory = await _context.TestCategories.FindAsync(categoryId);

            while (currentCategory != null)
            {
                result.Insert(0, currentCategory);

                if (currentCategory.ParentCategoryId.HasValue)
                {
                    currentCategory = await _context.TestCategories.FindAsync(currentCategory.ParentCategoryId);
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        public async Task<IEnumerable<TestTemplate>> GetTemplatesByCategoryAsync(Guid categoryId, bool includeChildCategories = false)
        {
            if (!includeChildCategories)
            {
                return await _context.TestTemplateCategories
                    .Where(tc => tc.TestCategoryId == categoryId)
                    .Select(tc => tc.TestTemplate)
                    .ToListAsync();
            }
            else
            {
                // Get all child category IDs recursively
                var categoryIds = new List<Guid> { categoryId };
                var childCategories = await GetAllChildCategoriesAsync(categoryId);
                categoryIds.AddRange(childCategories.Select(c => c.Id));

                // Get templates from all categories
                return await _context.TestTemplateCategories
                    .Where(tc => categoryIds.Contains(tc.TestCategoryId))
                    .Select(tc => tc.TestTemplate)
                    .Distinct()
                    .ToListAsync();
            }
        }

        private async Task<IEnumerable<TestCategory>> GetAllChildCategoriesAsync(Guid parentCategoryId)
        {
            var result = new List<TestCategory>();
            var directChildren = await GetChildCategoriesAsync(parentCategoryId);
            
            result.AddRange(directChildren);
            
            foreach (var child in directChildren)
            {
                var grandChildren = await GetAllChildCategoriesAsync(child.Id);
                result.AddRange(grandChildren);
            }
            
            return result;
        }
    }
}