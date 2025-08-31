using Microsoft.EntityFrameworkCore;
using Testing.Domain.Entities;
using Testing.Domain.Enums;
using Testing.Domain.Repositories;
using Testing.Infrastructure.Data;

namespace Testing.Infrastructure.Repositories
{
    public class TestTemplateRepository : Repository<TestTemplate>, ITestTemplateRepository
    {
        public TestTemplateRepository(TestingDbContext context) : base(context)
        {
        }

        public async Task<TestTemplate> GetByIdWithQuestionsAsync(Guid id)
        {
            return await _context.TestTemplates
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TestTemplate> GetByIdWithQuestionsAndCategoriesAndTagsAsync(Guid id)
        {
            return await _context.TestTemplates
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Options)
                .Include(t => t.TestTemplateCategories)
                    .ThenInclude(tc => tc.TestCategory)
                .Include(t => t.TestTemplateTags)
                    .ThenInclude(tt => tt.TestTag)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TestTemplate>> GetByCourseIdAsync(Guid courseId)
        {
            return await _context.TestTemplates
                .Where(t => t.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetByModuleIdAsync(Guid moduleId)
        {
            return await _context.TestTemplates
                .Where(t => t.ModuleId == moduleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetByLessonIdAsync(Guid lessonId)
        {
            return await _context.TestTemplates
                .Where(t => t.LessonId == lessonId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetByAuthorIdAsync(Guid authorId)
        {
            return await _context.TestTemplates
                .Where(t => t.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetByDifficultyLevelAsync(DifficultyLevel difficultyLevel)
        {
            return await _context.TestTemplates
                .Where(t => t.DifficultyLevel == difficultyLevel)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetActiveTemplatesAsync()
        {
            return await _context.TestTemplates
                .Where(t => t.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetPublishedTemplatesAsync()
        {
            return await _context.TestTemplates
                .Where(t => t.IsPublished)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetByCategoryAsync(Guid categoryId)
        {
            return await _context.TestTemplates
                .Include(t => t.TestTemplateCategories)
                .Where(t => t.TestTemplateCategories.Any(tc => tc.TestCategoryId == categoryId))
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetByTagAsync(Guid tagId)
        {
            return await _context.TestTemplates
                .Include(t => t.TestTemplateTags)
                .Where(t => t.TestTemplateTags.Any(tt => tt.TestTagId == tagId))
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> SearchTemplatesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await _context.TestTemplates
                .Where(t => t.Title.Contains(searchTerm) || 
                           t.Description.Contains(searchTerm) ||
                           t.Instructions.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task AddCategoryAsync(Guid testTemplateId, Guid categoryId)
        {
            var templateCategory = new TestTemplateCategory
            {
                TestTemplateId = testTemplateId,
                TestCategoryId = categoryId
            };

            await _context.TestTemplateCategories.AddAsync(templateCategory);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCategoryAsync(Guid testTemplateId, Guid categoryId)
        {
            var templateCategory = await _context.TestTemplateCategories
                .FirstOrDefaultAsync(tc => tc.TestTemplateId == testTemplateId && tc.TestCategoryId == categoryId);

            if (templateCategory != null)
            {
                _context.TestTemplateCategories.Remove(templateCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddTagAsync(Guid testTemplateId, Guid tagId)
        {
            var templateTag = new TestTemplateTag
            {
                TestTemplateId = testTemplateId,
                TestTagId = tagId
            };

            await _context.TestTemplateTags.AddAsync(templateTag);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTagAsync(Guid testTemplateId, Guid tagId)
        {
            var templateTag = await _context.TestTemplateTags
                .FirstOrDefaultAsync(tt => tt.TestTemplateId == testTemplateId && tt.TestTagId == tagId);

            if (templateTag != null)
            {
                _context.TestTemplateTags.Remove(templateTag);
                await _context.SaveChangesAsync();
            }
        }
    }
}