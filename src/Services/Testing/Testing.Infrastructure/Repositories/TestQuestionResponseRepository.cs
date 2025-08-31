using Microsoft.EntityFrameworkCore;
using Testing.Domain.Entities;
using Testing.Domain.Repositories;
using Testing.Infrastructure.Data;

namespace Testing.Infrastructure.Repositories
{
    public class TestQuestionResponseRepository : Repository<TestQuestionResponse>, ITestQuestionResponseRepository
    {
        public TestQuestionResponseRepository(TestingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TestQuestionResponse>> GetByTestAttemptIdAsync(Guid testAttemptId)
        {
            return await _context.TestQuestionResponses
                .Include(r => r.Question)
                .Include(r => r.OptionResponses)
                    .ThenInclude(so => so.Option)
                .Where(r => r.TestAttemptId == testAttemptId)
                .ToListAsync();
        }

        public async Task<TestQuestionResponse> GetByTestAttemptAndQuestionIdAsync(Guid testAttemptId, Guid questionId)
        {
            return await _context.TestQuestionResponses
                .Include(r => r.Question)
                .Include(r => r.OptionResponses)
                    .ThenInclude(so => so.Option)
                .FirstOrDefaultAsync(r => r.TestAttemptId == testAttemptId && r.QuestionId == questionId);
        }

        public async Task<IEnumerable<TestQuestionResponse>> GetCorrectResponsesAsync(Guid testAttemptId)
        {
            return await _context.TestQuestionResponses
                .Include(r => r.Question)
                .Where(r => r.TestAttemptId == testAttemptId && r.IsCorrect == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestQuestionResponse>> GetIncorrectResponsesAsync(Guid testAttemptId)
        {
            return await _context.TestQuestionResponses
                .Include(r => r.Question)
                .Where(r => r.TestAttemptId == testAttemptId && r.IsCorrect == false)
                .ToListAsync();
        }
    }
}