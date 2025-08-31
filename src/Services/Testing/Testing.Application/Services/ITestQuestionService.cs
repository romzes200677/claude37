using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Enums;

namespace Testing.Application.Services
{
    public interface ITestQuestionService
    {
        Task<TestQuestion> GetQuestionByIdAsync(Guid id);
        Task<IEnumerable<TestQuestion>> GetQuestionsByTestTemplateAsync(Guid testTemplateId);
        Task<IEnumerable<TestQuestion>> GetQuestionsByTypeAsync(Guid testTemplateId, QuestionType questionType);
        Task<IEnumerable<TestQuestion>> GetQuestionsByDifficultyAsync(Guid testTemplateId, DifficultyLevel difficultyLevel);
        Task<int> GetTotalPointsForTestAsync(Guid testTemplateId);
        Task<TestQuestion> CreateQuestionAsync(TestQuestion question);
        Task<TestQuestion> UpdateQuestionAsync(TestQuestion question);
        Task DeleteQuestionAsync(Guid id);
        Task ReorderQuestionsAsync(Guid testTemplateId, Dictionary<Guid, int> questionOrderMap);
    }
}