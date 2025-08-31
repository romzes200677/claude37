using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Enums;

namespace Testing.Domain.Repositories
{
    public interface ITestTemplateRepository : IRepository<TestTemplate>
    {
        Task<TestTemplate> GetByIdWithQuestionsAsync(Guid id);
        Task<TestTemplate> GetByIdWithQuestionsAndCategoriesAndTagsAsync(Guid id);
        Task<IEnumerable<TestTemplate>> GetByCourseIdAsync(Guid courseId);
        Task<IEnumerable<TestTemplate>> GetByModuleIdAsync(Guid moduleId);
        Task<IEnumerable<TestTemplate>> GetByLessonIdAsync(Guid lessonId);
        Task<IEnumerable<TestTemplate>> GetByAuthorIdAsync(Guid authorId);
        Task<IEnumerable<TestTemplate>> GetByDifficultyLevelAsync(DifficultyLevel difficultyLevel);
        Task<IEnumerable<TestTemplate>> GetActiveTemplatesAsync();
        Task<IEnumerable<TestTemplate>> GetPublishedTemplatesAsync();
        Task<IEnumerable<TestTemplate>> GetByCategoryAsync(Guid categoryId);
        Task<IEnumerable<TestTemplate>> GetByTagAsync(Guid tagId);
        Task<IEnumerable<TestTemplate>> SearchTemplatesAsync(string searchTerm);
    }
}