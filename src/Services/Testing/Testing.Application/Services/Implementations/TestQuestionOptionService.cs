using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Repositories;
using Testing.Application.Services;

namespace Testing.Application.Services.Implementations
{
    public class TestQuestionOptionService : ITestQuestionOptionService
    {
        private readonly ITestQuestionOptionRepository _testQuestionOptionRepository;
        private readonly ILogger<TestQuestionOptionService> _logger;

        public TestQuestionOptionService(
            ITestQuestionOptionRepository testQuestionOptionRepository,
            ILogger<TestQuestionOptionService> logger)
        {
            _testQuestionOptionRepository = testQuestionOptionRepository;
            _logger = logger;
        }

        public async Task<TestQuestionOption> GetOptionByIdAsync(Guid id)
        {
            var option = await _testQuestionOptionRepository.GetByIdAsync(id);
            if (option == null)
            {
                throw new KeyNotFoundException($"Test question option with ID {id} not found");
            }
            return option;
        }

        public async Task<IEnumerable<TestQuestionOption>> GetOptionsByQuestionAsync(Guid questionId)
        {
            return await _testQuestionOptionRepository.GetByQuestionIdAsync(questionId);
        }

        public async Task<IEnumerable<TestQuestionOption>> GetCorrectOptionsForQuestionAsync(Guid questionId)
        {
            return await _testQuestionOptionRepository.GetCorrectOptionsForQuestionAsync(questionId);
        }

        public async Task<TestQuestionOption> CreateOptionAsync(TestQuestionOption option)
        {
            try
            {
                option.CreatedAt = DateTime.UtcNow;
                option.UpdatedAt = DateTime.UtcNow;
                return await _testQuestionOptionRepository.AddAsync(option);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test question option");
                throw;
            }
        }

        public async Task<TestQuestionOption> UpdateOptionAsync(TestQuestionOption option)
        {
            try
            {
                var existingOption = await _testQuestionOptionRepository.GetByIdAsync(option.Id);
                if (existingOption == null)
                {
                    throw new KeyNotFoundException($"Test question option with ID {option.Id} not found");
                }

                option.UpdatedAt = DateTime.UtcNow;
                return await _testQuestionOptionRepository.UpdateAsync(option);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test question option");
                throw;
            }
        }

        public async Task DeleteOptionAsync(Guid id)
        {
            try
            {
                var option = await _testQuestionOptionRepository.GetByIdAsync(id);
                if (option == null)
                {
                    throw new KeyNotFoundException($"Test question option with ID {id} not found");
                }

                await _testQuestionOptionRepository.RemoveAsync(option);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test question option");
                throw;
            }
        }

        public async Task ReorderOptionsAsync(Guid questionId, Dictionary<Guid, int> optionOrderMap)
        {
            try
            {
                await _testQuestionOptionRepository.ReorderOptionsAsync(questionId, optionOrderMap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reordering test question options");
                throw;
            }
        }
    }
}