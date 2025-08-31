using Microsoft.EntityFrameworkCore;
using Testing.Domain.Entities;
using Testing.Domain.Repositories;
using Testing.Infrastructure.Data;

namespace Testing.Infrastructure.Repositories
{
    public class TestTagRepository : Repository<TestTag>, ITestTagRepository
    {
        public TestTagRepository(TestingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TestTag>> GetByNameAsync(string name)
        {
            return await _context.TestTags
                .Where(t => t.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTag>> GetByTestTemplateIdAsync(Guid testTemplateId)
        {
            return await _context.TestTemplateTags
                .Where(tt => tt.TestTemplateId == testTemplateId)
                .Select(tt => tt.TestTag)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestTemplate>> GetTemplatesByTagAsync(Guid tagId)
        {
            return await _context.TestTemplateTags
                .Where(tt => tt.TestTagId == tagId)
                .Select(tt => tt.TestTemplate)
                .ToListAsync();
        }
    }
}