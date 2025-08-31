using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Enums;

namespace Testing.Domain.Repositories
{
    public interface ITestQuestionRepository : IRepository<TestQuestion>
    {
        Task<IEnumerable<TestQuestion>> GetByTestTemplateIdAsync(Guid testTemplateId);
        Task<IEnumerable<TestQuestion>> GetByQuestionTypeAsync(Guid testTemplateId, QuestionType questionType);
        Task<IEnumerable<TestQuestion>> GetByDifficultyLevelAsync(Guid testTemplateId, DifficultyLevel difficultyLevel);
        Task<int> GetTotalPointsForTestAsync(Guid testTemplateId);
        Task ReorderQuestionsAsync(Guid testTemplateId, Dictionary<Guid, int> questionOrderMap);
        Task<TestQuestion> GetByIdWithOptionsAsync(Guid questionId);
    }
}