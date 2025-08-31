using Microsoft.EntityFrameworkCore;
using Testing.Domain.Entities;
using Testing.Domain.Repositories;
using Testing.Infrastructure.Data;

namespace Testing.Infrastructure.Repositories
{
    public class TestQuestionOptionResponseRepository : Repository<TestQuestionOptionResponse>, ITestQuestionOptionResponseRepository
    {
        public TestQuestionOptionResponseRepository(TestingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TestQuestionOptionResponse>> GetByQuestionResponseIdAsync(Guid questionResponseId)
        {
            return await _context.TestQuestionOptionResponses
                .Include(r => r.Option)
                .Where(r => r.QuestionResponseId == questionResponseId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestQuestionOptionResponse>> GetSelectedOptionsAsync(Guid questionResponseId)
        {
            return await _context.TestQuestionOptionResponses
                .Include(r => r.Option)
                .Where(r => r.QuestionResponseId == questionResponseId)
                .ToListAsync();
        }
    }
}