using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Enums;

namespace Testing.Application.Services
{
    public interface ITestTemplateService
    {
        Task<TestTemplate> GetTestTemplateByIdAsync(Guid id);
        Task<IEnumerable<TestTemplate>> GetAllTestTemplatesAsync();
        Task<IEnumerable<TestTemplate>> GetTestTemplatesByCourseAsync(Guid courseId);
        Task<IEnumerable<TestTemplate>> GetTestTemplatesByModuleAsync(Guid moduleId);
        Task<IEnumerable<TestTemplate>> GetTestTemplatesByLessonAsync(Guid lessonId);
        Task<IEnumerable<TestTemplate>> GetTestTemplatesByAuthorAsync(Guid authorId);
        Task<IEnumerable<TestTemplate>> GetTestTemplatesByDifficultyAsync(DifficultyLevel difficultyLevel);
        Task<IEnumerable<TestTemplate>> GetActiveTestTemplatesAsync();
        Task<IEnumerable<TestTemplate>> GetPublishedTestTemplatesAsync();
        Task<IEnumerable<TestTemplate>> GetTestTemplatesByCategoryAsync(Guid categoryId);
        Task<IEnumerable<TestTemplate>> GetTestTemplatesByTagAsync(Guid tagId);
        Task<IEnumerable<TestTemplate>> SearchTestTemplatesAsync(string searchTerm);
        Task<TestTemplate> CreateTestTemplateAsync(TestTemplate testTemplate);
        Task<TestTemplate> UpdateTestTemplateAsync(TestTemplate testTemplate);
        Task DeleteTestTemplateAsync(Guid id);
        Task<bool> PublishTestTemplateAsync(Guid id);
        Task<bool> UnpublishTestTemplateAsync(Guid id);
        Task<bool> ActivateTestTemplateAsync(Guid id);
        Task<bool> DeactivateTestTemplateAsync(Guid id);
        Task<bool> AssignCategoryToTemplateAsync(Guid templateId, Guid categoryId);
        Task<bool> RemoveCategoryFromTemplateAsync(Guid templateId, Guid categoryId);
        Task<bool> AssignTagToTemplateAsync(Guid templateId, Guid tagId);
        Task<bool> RemoveTagFromTemplateAsync(Guid templateId, Guid tagId);
    }
}