using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Repositories;
using Testing.Application.Services;

namespace Testing.Application.Services.Implementations
{
    public class TestTagService : ITestTagService
    {
        private readonly ITestTagRepository _testTagRepository;
        private readonly ILogger<TestTagService> _logger;

        public TestTagService(
            ITestTagRepository testTagRepository,
            ILogger<TestTagService> logger)
        {
            _testTagRepository = testTagRepository;
            _logger = logger;
        }

        public async Task<TestTag> GetTagByIdAsync(Guid id)
        {
            return await _testTagRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TestTag>> GetTagsByNameAsync(string name)
        {
            return await _testTagRepository.GetByNameAsync(name);
        }

        public async Task<IEnumerable<TestTag>> GetAllTagsAsync()
        {
            return await _testTagRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TestTag>> GetTagsByTestTemplateAsync(Guid testTemplateId)
        {
            return await _testTagRepository.GetByTestTemplateIdAsync(testTemplateId);
        }

        public async Task<IEnumerable<TestTemplate>> GetTemplatesByTagAsync(Guid tagId)
        {
            return await _testTagRepository.GetTemplatesByTagAsync(tagId);
        }

        public async Task<TestTag> CreateTagAsync(TestTag tag)
        {
            try
            {
                // Check if a tag with the same name already exists
                var existingTags = await _testTagRepository.GetByNameAsync(tag.Name);
                if (existingTags.Any())
                {
                    throw new InvalidOperationException($"A tag with the name '{tag.Name}' already exists");
                }

                tag.CreatedAt = DateTime.UtcNow;
                tag.UpdatedAt = DateTime.UtcNow;
                return await _testTagRepository.AddAsync(tag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test tag");
                throw;
            }
        }

        public async Task<TestTag> UpdateTagAsync(TestTag tag)
        {
            try
            {
                var existingTag = await _testTagRepository.GetByIdAsync(tag.Id);
                if (existingTag == null)
                {
                    throw new KeyNotFoundException($"Test tag with ID {tag.Id} not found");
                }

                // Check if a different tag with the same name already exists
                var tagsWithSameName = await _testTagRepository.GetByNameAsync(tag.Name);
                var tagWithSameName = tagsWithSameName.FirstOrDefault();
                if (tagWithSameName != null && tagWithSameName.Id != tag.Id)
                {
                    throw new InvalidOperationException($"A different tag with the name '{tag.Name}' already exists");
                }

                tag.UpdatedAt = DateTime.UtcNow;
                return await _testTagRepository.UpdateAsync(tag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test tag");
                throw;
            }
        }

        public async Task DeleteTagAsync(Guid id)
        {
            try
            {
                var tag = await _testTagRepository.GetByIdAsync(id);
                if (tag == null)
                {
                    throw new KeyNotFoundException($"Test tag with ID {id} not found");
                }

                // Check if tag has associated test templates
                var testTemplates = await _testTagRepository.GetTemplatesByTagAsync(id);
                if (testTemplates.Any())
                {
                    // Instead of throwing an exception, we could remove the tag from all associated test templates
                    // This depends on the business requirements
                    _logger.LogWarning($"Deleting tag '{tag.Name}' that has {testTemplates.Count()} associated test templates");
                }

                await _testTagRepository.RemoveAsync(tag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test tag");
                throw;
            }
        }

        // Этот метод не определен в интерфейсе ITestTagService, поэтому делаем его приватным
        private async Task<TestTag> GetOrCreateAsync(string name)
        {
            try
            {
                var existingTags = await _testTagRepository.GetByNameAsync(name);
                var existingTag = existingTags.FirstOrDefault();
                if (existingTag != null)
                {
                    return existingTag;
                }

                var newTag = new TestTag
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                return await _testTagRepository.AddAsync(newTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting or creating test tag");
                throw;
            }
        }
    }
}