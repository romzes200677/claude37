using Microsoft.EntityFrameworkCore;
using Testing.Domain.Entities;
using Testing.Domain.Repositories;
using Testing.Infrastructure.Data;

namespace Testing.Infrastructure.Repositories
{
    public class TestQuestionOptionRepository : Repository<TestQuestionOption>, ITestQuestionOptionRepository
    {
        public TestQuestionOptionRepository(TestingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TestQuestionOption>> GetByQuestionIdAsync(Guid questionId)
        {
            return await _context.TestQuestionOptions
                .Where(o => o.QuestionId == questionId)
                .OrderBy(o => o.OrderIndex)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestQuestionOption>> GetCorrectOptionsForQuestionAsync(Guid questionId)
        {
            return await _context.TestQuestionOptions
                .Where(o => o.QuestionId == questionId && o.IsCorrect)
                .OrderBy(o => o.OrderIndex)
                .ToListAsync();
        }

        public async Task ReorderOptionsAsync(Guid questionId, Dictionary<Guid, int> optionOrderMap)
        {
            var options = await _context.TestQuestionOptions
                .Where(o => o.QuestionId == questionId)
                .ToListAsync();

            foreach (var kvp in optionOrderMap)
            {
                var option = options.FirstOrDefault(o => o.Id == kvp.Key);
                if (option != null)
                {
                    option.OrderIndex = kvp.Value;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}