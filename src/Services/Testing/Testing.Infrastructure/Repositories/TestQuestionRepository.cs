using Microsoft.EntityFrameworkCore;
using Testing.Domain.Entities;
using Testing.Domain.Enums;
using Testing.Domain.Repositories;
using Testing.Infrastructure.Data;

namespace Testing.Infrastructure.Repositories
{
    public class TestQuestionRepository : Repository<TestQuestion>, ITestQuestionRepository
    {
        public TestQuestionRepository(TestingDbContext context) : base(context)
        {
        }

        public async Task<TestQuestion> GetByIdWithOptionsAsync(Guid id)
        {
            return await _context.TestQuestions
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<TestQuestion>> GetByTestTemplateIdAsync(Guid testTemplateId)
        {
            return await _context.TestQuestions
                .Include(q => q.Options)
                .Where(q => q.TestTemplateId == testTemplateId)
                .OrderBy(q => q.OrderIndex)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestQuestion>> GetByQuestionTypeAsync(Guid testTemplateId, QuestionType questionType)
        {
            return await _context.TestQuestions
                .Include(q => q.Options)
                .Where(q => q.TestTemplateId == testTemplateId && q.QuestionType == questionType)
                .OrderBy(q => q.OrderIndex)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestQuestion>> GetByDifficultyLevelAsync(Guid testTemplateId, DifficultyLevel difficultyLevel)
        {
            return await _context.TestQuestions
                .Include(q => q.Options)
                .Where(q => q.TestTemplateId == testTemplateId && q.DifficultyLevel == difficultyLevel)
                .OrderBy(q => q.OrderIndex)
                .ToListAsync();
        }

        public async Task<int> GetTotalPointsForTestAsync(Guid testTemplateId)
        {
            return await _context.TestQuestions
                .Where(q => q.TestTemplateId == testTemplateId)
                .SumAsync(q => q.Points);
        }

        public async Task ReorderQuestionsAsync(Guid testTemplateId, Dictionary<Guid, int> questionOrderMap)
        {
            var questions = await _context.TestQuestions
                .Where(q => q.TestTemplateId == testTemplateId)
                .ToListAsync();

            foreach (var kvp in questionOrderMap)
            {
                var question = questions.FirstOrDefault(q => q.Id == kvp.Key);
                if (question != null)
                {
                    question.OrderIndex = kvp.Value;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}