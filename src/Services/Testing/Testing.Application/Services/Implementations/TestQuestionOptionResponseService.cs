using Microsoft.Extensions.Logging;
using Testing.Domain.Entities;
using Testing.Domain.Repositories;
using Testing.Application.Services;

namespace Testing.Application.Services.Implementations
{
    public class TestQuestionOptionResponseService : ITestQuestionOptionResponseService
    {
        private readonly ITestQuestionOptionResponseRepository _testQuestionOptionResponseRepository;
        private readonly ILogger<TestQuestionOptionResponseService> _logger;

        public TestQuestionOptionResponseService(
            ITestQuestionOptionResponseRepository testQuestionOptionResponseRepository,
            ILogger<TestQuestionOptionResponseService> logger)
        {
            _testQuestionOptionResponseRepository = testQuestionOptionResponseRepository;
            _logger = logger;
        }

        public async Task<TestQuestionOptionResponse> GetByIdAsync(Guid id)
        {
            return await _testQuestionOptionResponseRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Test question option response with ID {id} not found");
        }

        public async Task<IEnumerable<TestQuestionOptionResponse>> GetByQuestionResponseIdAsync(Guid questionResponseId)
        {
            return await _testQuestionOptionResponseRepository.GetByQuestionResponseIdAsync(questionResponseId);
        }

        public async Task<TestQuestionOptionResponse> CreateAsync(TestQuestionOptionResponse optionResponse)
        {
            try
            {
                optionResponse.CreatedAt = DateTime.UtcNow;
                optionResponse.UpdatedAt = DateTime.UtcNow;
                return await _testQuestionOptionResponseRepository.AddAsync(optionResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test question option response");
                throw;
            }
        }

        public async Task<TestQuestionOptionResponse> UpdateAsync(TestQuestionOptionResponse optionResponse)
        {
            try
            {
                var existingOptionResponse = await _testQuestionOptionResponseRepository.GetByIdAsync(optionResponse.Id);
                if (existingOptionResponse == null)
                {
                    throw new KeyNotFoundException($"Test question option response with ID {optionResponse.Id} not found");
                }

                optionResponse.UpdatedAt = DateTime.UtcNow;
                return await _testQuestionOptionResponseRepository.UpdateAsync(optionResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test question option response");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var optionResponse = await _testQuestionOptionResponseRepository.GetByIdAsync(id);
                if (optionResponse == null)
                {
                    throw new KeyNotFoundException($"Test question option response with ID {id} not found");
                }

                await _testQuestionOptionResponseRepository.RemoveAsync(optionResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test question option response");
                throw;
            }
        }

        public async Task<IEnumerable<TestQuestionOptionResponse>> CreateRangeAsync(IEnumerable<TestQuestionOptionResponse> optionResponses)
        {
            try
            {
                foreach (var optionResponse in optionResponses)
                {
                    optionResponse.CreatedAt = DateTime.UtcNow;
                    optionResponse.UpdatedAt = DateTime.UtcNow;
                }

                return await _testQuestionOptionResponseRepository.AddRangeAsync(optionResponses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating multiple test question option responses");
                throw;
            }
        }

        public async Task DeleteByQuestionResponseIdAsync(Guid questionResponseId)
        {
            try
            {
                var optionResponses = await _testQuestionOptionResponseRepository.GetByQuestionResponseIdAsync(questionResponseId);
                if (optionResponses.Any())
                {
                    await _testQuestionOptionResponseRepository.RemoveRangeAsync(optionResponses);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test question option responses by question response ID");
                throw;
            }
        }
    }
}