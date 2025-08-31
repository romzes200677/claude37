using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Enums;
using Testing.Domain.Repositories;
using Testing.Application.Services;

namespace Testing.Application.Services.Implementations
{
    public class TestQuestionService : ITestQuestionService
    {
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly ILogger<TestQuestionService> _logger;

        public TestQuestionService(
            ITestQuestionRepository testQuestionRepository,
            ILogger<TestQuestionService> logger)
        {
            _testQuestionRepository = testQuestionRepository;
            _logger = logger;
        }

        public async Task<TestQuestion> GetQuestionByIdAsync(Guid id)
        {
            return await _testQuestionRepository.GetByIdAsync(id);
        }

        // Этот метод не определен в интерфейсе ITestQuestionService, поэтому делаем его приватным
        private async Task<TestQuestion?> GetByIdWithOptionsAsync(Guid id)
        {
            return await _testQuestionRepository.GetByIdWithOptionsAsync(id);
        }

        public async Task<IEnumerable<TestQuestion>> GetQuestionsByTestTemplateAsync(Guid testTemplateId)
        {
            return await _testQuestionRepository.GetByTestTemplateIdAsync(testTemplateId);
        }

        public async Task<IEnumerable<TestQuestion>> GetQuestionsByTypeAsync(Guid testTemplateId, QuestionType questionType)
        {
            return await _testQuestionRepository.GetByQuestionTypeAsync(testTemplateId, questionType);
        }

        public async Task<IEnumerable<TestQuestion>> GetQuestionsByDifficultyAsync(Guid testTemplateId, DifficultyLevel difficultyLevel)
        {
            return await _testQuestionRepository.GetByDifficultyLevelAsync(testTemplateId, difficultyLevel);
        }

        public async Task<int> GetTotalPointsForTestAsync(Guid testTemplateId)
        {
            return await _testQuestionRepository.GetTotalPointsForTestAsync(testTemplateId);
        }

        public async Task<TestQuestion> CreateQuestionAsync(TestQuestion question)
        {
            try
            {
                question.CreatedAt = DateTime.UtcNow;
                question.UpdatedAt = DateTime.UtcNow;
                return await _testQuestionRepository.AddAsync(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test question");
                throw;
            }
        }

        public async Task<TestQuestion> UpdateQuestionAsync(TestQuestion question)
        {
            try
            {
                var existingQuestion = await _testQuestionRepository.GetByIdAsync(question.Id);
                if (existingQuestion == null)
                {
                    throw new KeyNotFoundException($"Test question with ID {question.Id} not found");
                }

                question.UpdatedAt = DateTime.UtcNow;
                return await _testQuestionRepository.UpdateAsync(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test question");
                throw;
            }
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            try
            {
                var question = await _testQuestionRepository.GetByIdAsync(id);
                if (question == null)
                {
                    throw new KeyNotFoundException($"Test question with ID {id} not found");
                }

                await _testQuestionRepository.RemoveAsync(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test question");
                throw;
            }
        }

        public async Task ReorderQuestionsAsync(Guid testTemplateId, Dictionary<Guid, int> questionOrderMap)
        {
            try
            {
                await _testQuestionRepository.ReorderQuestionsAsync(testTemplateId, questionOrderMap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reordering test questions");
                throw;
            }
        }
    }
}