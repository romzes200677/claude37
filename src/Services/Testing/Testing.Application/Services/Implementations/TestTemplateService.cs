using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Enums;
using Testing.Domain.Repositories;
using Testing.Application.Services;

namespace Testing.Application.Services.Implementations
{
    public class TestTemplateService : ITestTemplateService
    {
        private readonly ITestTemplateRepository _testTemplateRepository;
        private readonly ILogger<TestTemplateService> _logger;

        public TestTemplateService(
            ITestTemplateRepository testTemplateRepository,
            ILogger<TestTemplateService> logger)
        {
            _testTemplateRepository = testTemplateRepository;
            _logger = logger;
        }

        public async Task<TestTemplate> GetTestTemplateByIdAsync(Guid id)
        {
            return await _testTemplateRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TestTemplate>> GetAllTestTemplatesAsync()
        {
            return await _testTemplateRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetTestTemplatesByCourseAsync(Guid courseId)
        {
            return await _testTemplateRepository.GetByCourseIdAsync(courseId);
        }

        public async Task<IEnumerable<TestTemplate>> GetTestTemplatesByModuleAsync(Guid moduleId)
        {
            return await _testTemplateRepository.GetByModuleIdAsync(moduleId);
        }

        public async Task<IEnumerable<TestTemplate>> GetTestTemplatesByLessonAsync(Guid lessonId)
        {
            return await _testTemplateRepository.GetByLessonIdAsync(lessonId);
        }

        public async Task<IEnumerable<TestTemplate>> GetTestTemplatesByAuthorAsync(Guid authorId)
        {
            return await _testTemplateRepository.GetByAuthorIdAsync(authorId);
        }

        public async Task<IEnumerable<TestTemplate>> GetTestTemplatesByDifficultyAsync(DifficultyLevel difficultyLevel)
        {
            return await _testTemplateRepository.GetByDifficultyLevelAsync(difficultyLevel);
        }

        public async Task<IEnumerable<TestTemplate>> GetActiveTestTemplatesAsync()
        {
            return await _testTemplateRepository.GetActiveTemplatesAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetPublishedTestTemplatesAsync()
        {
            return await _testTemplateRepository.GetPublishedTemplatesAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetTestTemplatesByCategoryAsync(Guid categoryId)
        {
            return await _testTemplateRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<TestTemplate>> GetTestTemplatesByTagAsync(Guid tagId)
        {
            return await _testTemplateRepository.GetByTagAsync(tagId);
        }

        public async Task<IEnumerable<TestTemplate>> SearchTestTemplatesAsync(string searchTerm)
        {
            return await _testTemplateRepository.SearchTemplatesAsync(searchTerm);
        }

        public async Task<TestTemplate> CreateTestTemplateAsync(TestTemplate testTemplate)
        {
            try
            {
                testTemplate.CreatedAt = DateTime.UtcNow;
                testTemplate.UpdatedAt = DateTime.UtcNow;
                return await _testTemplateRepository.AddAsync(testTemplate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning category to template");
                throw;
            }
        }

        public async Task<bool> RemoveCategoryFromTemplateAsync(Guid templateId, Guid categoryId)
        {
            try
            {
                var template = await _testTemplateRepository.GetByIdWithQuestionsAndCategoriesAndTagsAsync(templateId);
                if (template == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {templateId} not found");
                }

                // Check if the category is assigned
                var categoryAssignment = template.TestTemplateCategories?.FirstOrDefault(tc => tc.TestCategoryId == categoryId);
                if (categoryAssignment == null)
                {
                    return false; // Category not assigned
                }

                // Remove the category
                if (template.TestTemplateCategories != null)
                {
                    template.TestTemplateCategories.Remove(categoryAssignment);
                    template.UpdatedAt = DateTime.UtcNow;
                    await _testTemplateRepository.UpdateAsync(template);
                    return true;
                }
                return false; // TestTemplateCategories is null
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing category from template");
                throw;
            }
        }

        public async Task<bool> AssignTagToTemplateAsync(Guid templateId, Guid tagId)
        {
            try
            {
                var template = await _testTemplateRepository.GetByIdWithQuestionsAndCategoriesAndTagsAsync(templateId);
                if (template == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {templateId} not found");
                }

                // Check if the tag is already assigned
                if (template.TestTemplateTags != null && 
                    template.TestTemplateTags.Any(tt => tt.TestTagId == tagId))
                {
                    return false; // Tag already assigned
                }

                // Add the tag
                if (template.TestTemplateTags == null)
                {
                    template.TestTemplateTags = new List<TestTemplateTag>();
                }

                template.TestTemplateTags.Add(new TestTemplateTag
                {
                    TestTemplateId = templateId,
                    TestTagId = tagId
                });

                template.UpdatedAt = DateTime.UtcNow;
                await _testTemplateRepository.UpdateAsync(template);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning tag to template");
                throw;
            }
        }

        public async Task<bool> RemoveTagFromTemplateAsync(Guid templateId, Guid tagId)
        {
            try
            {
                var template = await _testTemplateRepository.GetByIdWithQuestionsAndCategoriesAndTagsAsync(templateId);
                if (template == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {templateId} not found");
                }

                // Check if the tag is assigned
                var tagAssignment = template.TestTemplateTags?.FirstOrDefault(tt => tt.TestTagId == tagId);
                if (tagAssignment == null)
                {
                    return false; // Tag not assigned
                }

                // Remove the tag
                if (template.TestTemplateTags != null)
                {
                    template.TestTemplateTags.Remove(tagAssignment);
                    template.UpdatedAt = DateTime.UtcNow;
                    await _testTemplateRepository.UpdateAsync(template);
                    return true;
                }
                return false; // TestTemplateTags is null
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing tag from template");
                throw;
            }
        }

        public async Task<TestTemplate> UpdateTestTemplateAsync(TestTemplate testTemplate)
        {
            try
            {
                var existingTemplate = await _testTemplateRepository.GetByIdAsync(testTemplate.Id);
                if (existingTemplate == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {testTemplate.Id} not found");
                }

                testTemplate.UpdatedAt = DateTime.UtcNow;
                return await _testTemplateRepository.UpdateAsync(testTemplate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test template");
                throw;
            }
        }

        public async Task DeleteTestTemplateAsync(Guid id)
        {
            try
            {
                var template = await _testTemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {id} not found");
                }

                await _testTemplateRepository.RemoveAsync(template);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test template");
                throw;
            }
        }

        public async Task<bool> PublishTestTemplateAsync(Guid id)
        {
            try
            {
                var template = await _testTemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {id} not found");
                }

                template.IsPublished = true;
                template.UpdatedAt = DateTime.UtcNow;
                await _testTemplateRepository.UpdateAsync(template);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing test template");
                throw;
            }
        }

        public async Task<bool> UnpublishTestTemplateAsync(Guid id)
        {
            try
            {
                var template = await _testTemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {id} not found");
                }

                template.IsPublished = false;
                template.UpdatedAt = DateTime.UtcNow;
                await _testTemplateRepository.UpdateAsync(template);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unpublishing test template");
                throw;
            }
        }

        public async Task<bool> ActivateTestTemplateAsync(Guid id)
        {
            try
            {
                var template = await _testTemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {id} not found");
                }

                template.IsActive = true;
                template.UpdatedAt = DateTime.UtcNow;
                await _testTemplateRepository.UpdateAsync(template);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating test template");
                throw;
            }
        }

        public async Task<bool> DeactivateTestTemplateAsync(Guid id)
        {
            try
            {
                var template = await _testTemplateRepository.GetByIdAsync(id);
                if (template == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {id} not found");
                }

                template.IsActive = false;
                template.UpdatedAt = DateTime.UtcNow;
                await _testTemplateRepository.UpdateAsync(template);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating test template");
                throw;
            }
        }

        public async Task<bool> AssignCategoryToTemplateAsync(Guid templateId, Guid categoryId)
        {
            try
            {
                var template = await _testTemplateRepository.GetByIdWithQuestionsAndCategoriesAndTagsAsync(templateId);
                if (template == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {templateId} not found");
                }

                // Check if the category is already assigned
                if (template.TestTemplateCategories != null && 
                    template.TestTemplateCategories.Any(tc => tc.TestCategoryId == categoryId))
                {
                    return false; // Category already assigned
                }

                // Add the category
                if (template.TestTemplateCategories == null)
                {
                    template.TestTemplateCategories = new List<TestTemplateCategory>();
                }

                template.TestTemplateCategories.Add(new TestTemplateCategory
                {
                    TestTemplateId = templateId,
                    TestCategoryId = categoryId
                });

                template.UpdatedAt = DateTime.UtcNow;
                await _testTemplateRepository.UpdateAsync(template);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test template");
                throw;
            }
        }

        // Методы UpdateAsync, DeleteAsync, PublishAsync, UnpublishAsync, ActivateAsync и DeactivateAsync удалены,
        // так как они дублируют функциональность соответствующих методов с префиксом TestTemplate
        }

        // Метод AddCategoryToTemplateAsync удален, так как он дублирует функциональность метода AssignCategoryToTemplateAsync
        // Методы AddCategoryAsync и RemoveCategoryAsync удалены, так как они не определены в интерфейсе ITestTemplateService

        // Метод AddTagToTemplateAsync удален, так как он дублирует функциональность метода AssignTagToTemplateAsync
        // Методы AddTagAsync и RemoveTagAsync удалены, так как они не определены в интерфейсе ITestTemplateService
    }