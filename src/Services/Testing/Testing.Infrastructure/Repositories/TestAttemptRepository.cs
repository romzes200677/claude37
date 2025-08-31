using Microsoft.EntityFrameworkCore;
using Testing.Domain.Entities;
using Testing.Domain.Enums;
using Testing.Domain.Repositories;
using Testing.Infrastructure.Data;

namespace Testing.Infrastructure.Repositories
{
    public class TestAttemptRepository : Repository<TestAttempt>, ITestAttemptRepository
    {
        public TestAttemptRepository(TestingDbContext context) : base(context)
        {
        }

        public async Task<TestAttempt> GetByIdWithResponsesAsync(Guid id)
        {
            return await _context.TestAttempts
                .Include(a => a.QuestionResponses)
                    .ThenInclude(qr => qr.OptionResponses)
                        .ThenInclude(so => so.Option)
                .Include(a => a.QuestionResponses)
                    .ThenInclude(qr => qr.Question)
                .Include(a => a.TestTemplate)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<TestAttempt>> GetByUserIdAsync(Guid userId)
        {
            return await _context.TestAttempts
                .Include(a => a.TestTemplate)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.StartedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestAttempt>> GetByTestTemplateIdAsync(Guid testTemplateId)
        {
            return await _context.TestAttempts
                .Where(a => a.TestTemplateId == testTemplateId)
                .OrderByDescending(a => a.StartedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestAttempt>> GetByUserAndTestTemplateAsync(Guid userId, Guid testTemplateId)
        {
            return await _context.TestAttempts
                .Where(a => a.UserId == userId && a.TestTemplateId == testTemplateId)
                .OrderByDescending(a => a.StartedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestAttempt>> GetByStatusAsync(TestAttemptStatus status)
        {
            return await _context.TestAttempts
                .Include(a => a.TestTemplate)
                .Where(a => a.Status == status)
                .OrderByDescending(a => a.StartedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestAttempt>> GetByReviewStatusAsync(ReviewStatus reviewStatus)
        {
            return await _context.TestAttempts
                .Include(a => a.TestTemplate)
                .Where(a => a.ReviewStatus == reviewStatus)
                .OrderByDescending(a => a.StartedAt)
                .ToListAsync();
        }

        public async Task<int> GetAttemptCountAsync(Guid userId, Guid testTemplateId)
        {
            return await _context.TestAttempts
                .CountAsync(a => a.UserId == userId && a.TestTemplateId == testTemplateId);
        }

        public async Task<TestAttempt?> GetLatestAttemptAsync(Guid userId, Guid testTemplateId)
        {
            return await _context.TestAttempts
                .Where(a => a.UserId == userId && a.TestTemplateId == testTemplateId)
                .OrderByDescending(a => a.StartedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TestAttempt>> GetCompletedAttemptsAsync(Guid userId, Guid testTemplateId)
        {
            return await _context.TestAttempts
                .Where(a => a.UserId == userId && 
                           a.TestTemplateId == testTemplateId && 
                           a.Status == TestAttemptStatus.Completed)
                .OrderByDescending(a => a.CompletedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestAttempt>> GetPassedAttemptsAsync(Guid userId, Guid testTemplateId)
        {
            var testTemplate = await _context.TestTemplates.FindAsync(testTemplateId);
            if (testTemplate == null)
                return new List<TestAttempt>();

            return await _context.TestAttempts
                .Where(a => a.UserId == userId && 
                           a.TestTemplateId == testTemplateId && 
                           a.Status == TestAttemptStatus.Completed &&
                           a.Score >= testTemplate.PassingScore)
                .OrderByDescending(a => a.CompletedAt)
                .ToListAsync();
        }
    }
}