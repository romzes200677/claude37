using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Repositories;
using Testing.Application.Services;

namespace Testing.Application.Services.Implementations
{
    public class TestCategoryService : ITestCategoryService
    {
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly ILogger<TestCategoryService> _logger;

        public TestCategoryService(
            ITestCategoryRepository testCategoryRepository,
            ILogger<TestCategoryService> logger)
        {
            _testCategoryRepository = testCategoryRepository;
            _logger = logger;
        }

        public async Task<TestCategory> GetCategoryByIdAsync(Guid id)
        {
            return await _testCategoryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TestCategory>> GetAllCategoriesAsync()
        {
            return await _testCategoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TestCategory>> GetRootCategoriesAsync()
        {
            return await _testCategoryRepository.GetRootCategoriesAsync();
        }

        public async Task<IEnumerable<TestCategory>> GetChildCategoriesAsync(Guid parentId)
        {
            return await _testCategoryRepository.GetChildCategoriesAsync(parentId);
        }

        public async Task<IEnumerable<TestCategory>> GetCategoryPathAsync(Guid categoryId)
        {
            return await _testCategoryRepository.GetCategoryPathAsync(categoryId);
        }

        public async Task<IEnumerable<TestTemplate>> GetTemplatesByCategoryAsync(Guid categoryId, bool includeChildCategories = false)
        {
            if (!includeChildCategories)
            {
                return await _testCategoryRepository.GetTemplatesByCategoryAsync(categoryId);
            }
            
            // Get templates from this category
            var templates = await _testCategoryRepository.GetTemplatesByCategoryAsync(categoryId);
            var result = new List<TestTemplate>(templates);
            
            // Get child categories recursively
            var childCategories = await _testCategoryRepository.GetChildCategoriesAsync(categoryId);
            foreach (var childCategory in childCategories)
            {
                var childTemplates = await GetTemplatesByCategoryAsync(childCategory.Id, true);
                result.AddRange(childTemplates);
            }
            
            return result;
        }

        public async Task<TestCategory> CreateCategoryAsync(TestCategory category)
        {
            try
            {
                // Validate parent category if specified
                if (category.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _testCategoryRepository.GetByIdAsync(category.ParentCategoryId.Value);
                    if (parentCategory == null)
                    {
                        throw new KeyNotFoundException($"Parent category with ID {category.ParentCategoryId.Value} not found");
                    }
                }

                category.CreatedAt = DateTime.UtcNow;
                category.UpdatedAt = DateTime.UtcNow;
                return await _testCategoryRepository.AddAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test category");
                throw;
            }
        }

        public async Task<TestCategory> UpdateCategoryAsync(TestCategory category)
        {
            try
            {
                var existingCategory = await _testCategoryRepository.GetByIdAsync(category.Id);
                if (existingCategory == null)
                {
                    throw new KeyNotFoundException($"Test category with ID {category.Id} not found");
                }

                // Validate parent category if specified
                if (category.ParentCategoryId.HasValue)
                {
                    // Prevent circular references
                    if (category.ParentCategoryId.Value == category.Id)
                    {
                        throw new InvalidOperationException("A category cannot be its own parent");
                    }

                    var parentCategory = await _testCategoryRepository.GetByIdAsync(category.ParentCategoryId.Value);
                    if (parentCategory == null)
                    {
                        throw new KeyNotFoundException($"Parent category with ID {category.ParentCategoryId.Value} not found");
                    }

                    // Check if the new parent is not a descendant of this category
                    var categoryPath = await _testCategoryRepository.GetCategoryPathAsync(category.ParentCategoryId.Value);
                    if (categoryPath.Any(c => c.Id == category.Id))
                    {
                        throw new InvalidOperationException("Cannot set a descendant as the parent category");
                    }
                }

                category.UpdatedAt = DateTime.UtcNow;
                return await _testCategoryRepository.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test category");
                throw;
            }
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            try
            {
                var category = await _testCategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    throw new KeyNotFoundException($"Test category with ID {id} not found");
                }

                // Check if category has children
                var children = await _testCategoryRepository.GetChildCategoriesAsync(id);
                if (children.Any())
                {
                    throw new InvalidOperationException("Cannot delete a category that has child categories");
                }

                // Check if category has associated test templates
                var testTemplates = await _testCategoryRepository.GetTemplatesByCategoryAsync(id);
                if (testTemplates.Any())
                {
                    throw new InvalidOperationException("Cannot delete a category that has associated test templates");
                }

                await _testCategoryRepository.RemoveAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test category");
                throw;
            }
        }
    }
}