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
    public class TestQuestionResponseService : ITestQuestionResponseService
    {
        private readonly ITestQuestionResponseRepository _testQuestionResponseRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly ITestQuestionOptionRepository _testQuestionOptionRepository;
        private readonly ILogger<TestQuestionResponseService> _logger;

        public TestQuestionResponseService(
            ITestQuestionResponseRepository testQuestionResponseRepository,
            ITestQuestionRepository testQuestionRepository,
            ITestQuestionOptionRepository testQuestionOptionRepository,
            ILogger<TestQuestionResponseService> logger)
        {
            _testQuestionResponseRepository = testQuestionResponseRepository;
            _testQuestionRepository = testQuestionRepository;
            _testQuestionOptionRepository = testQuestionOptionRepository;
            _logger = logger;
        }

        public async Task<TestQuestionResponse> GetResponseByIdAsync(Guid id)
        {
            return await _testQuestionResponseRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TestQuestionResponse>> GetResponsesByAttemptAsync(Guid attemptId)
        {
            return await _testQuestionResponseRepository.GetByTestAttemptIdAsync(attemptId);
        }

        public async Task<TestQuestionResponse> GetResponseByAttemptAndQuestionAsync(Guid attemptId, Guid questionId)
        {
            return await _testQuestionResponseRepository.GetByTestAttemptAndQuestionIdAsync(attemptId, questionId);
        }

        public async Task<IEnumerable<TestQuestionResponse>> GetCorrectResponsesAsync(Guid testAttemptId)
        {
            return await _testQuestionResponseRepository.GetCorrectResponsesAsync(testAttemptId);
        }

        public async Task<IEnumerable<TestQuestionResponse>> GetIncorrectResponsesAsync(Guid testAttemptId)
        {
            return await _testQuestionResponseRepository.GetIncorrectResponsesAsync(testAttemptId);
        }

        public async Task<TestQuestionResponse> SaveTextResponseAsync(Guid attemptId, Guid questionId, string responseText)
        {
            try
            {
                // Validate that the question exists
                var question = await _testQuestionRepository.GetByIdAsync(questionId);
                if (question == null)
                {
                    throw new KeyNotFoundException($"Test question with ID {questionId} not found");
                }

                // Check if a response already exists for this attempt and question
                var existingResponse = await _testQuestionResponseRepository.GetByTestAttemptAndQuestionIdAsync(attemptId, questionId);
                if (existingResponse != null)
                {
                    // Update existing response
                    existingResponse.ResponseText = responseText;
                    existingResponse.UpdatedAt = DateTime.UtcNow;

                    // Re-evaluate if the response is correct
                    await EvaluateResponseInternalAsync(existingResponse);

                    return await _testQuestionResponseRepository.UpdateAsync(existingResponse);
                }
                else
                {
                    // Create new response
                    var response = new TestQuestionResponse
                    {
                        Id = Guid.NewGuid(),
                        TestAttemptId = attemptId,
                        QuestionId = questionId,
                        ResponseText = responseText,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    // Evaluate if the response is correct
                    await EvaluateResponseInternalAsync(response);

                    return await _testQuestionResponseRepository.AddAsync(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving text response");
                throw;
            }
        }

        public async Task<TestQuestionResponse> SaveOptionResponsesAsync(Guid attemptId, Guid questionId, IEnumerable<Guid> selectedOptionIds)
        {
            try
            {
                // Validate that the question exists
                var question = await _testQuestionRepository.GetByIdAsync(questionId);
                if (question == null)
                {
                    throw new KeyNotFoundException($"Test question with ID {questionId} not found");
                }

                // Validate that all selected options exist and belong to this question
                foreach (var optionId in selectedOptionIds)
                {
                    var option = await _testQuestionOptionRepository.GetByIdAsync(optionId);
                    if (option == null || option.QuestionId != questionId)
                    {
                        throw new ArgumentException($"Option with ID {optionId} does not exist or does not belong to question {questionId}");
                    }
                }

                // Check if a response already exists for this attempt and question
                var existingResponse = await _testQuestionResponseRepository.GetByTestAttemptAndQuestionIdAsync(attemptId, questionId);
                if (existingResponse != null)
                {
                    // Update existing response
                    // First, clear existing selected options
                    if (existingResponse.OptionResponses != null)
                    {
                        existingResponse.OptionResponses.Clear();
                    }
                    
                    // Then add new selected options
                    // Инициализируем коллекцию OptionResponses, если она null
                    if (existingResponse.OptionResponses == null)
                    {
                        existingResponse.OptionResponses = new List<TestQuestionOptionResponse>();
                    }
                    
                    foreach (var optionId in selectedOptionIds)
                    {
                        existingResponse.OptionResponses.Add(new TestQuestionOptionResponse
                        {
                            Id = Guid.NewGuid(),
                            QuestionResponseId = existingResponse.Id,
                            OptionId = optionId,
                            IsSelected = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        });
                    }
                    
                    existingResponse.UpdatedAt = DateTime.UtcNow;

                    // Re-evaluate if the response is correct
                    await EvaluateResponseInternalAsync(existingResponse);

                    return await _testQuestionResponseRepository.UpdateAsync(existingResponse);
                }
                else
                {
                    // Create new response
                    var response = new TestQuestionResponse
                    {
                        Id = Guid.NewGuid(),
                        TestAttemptId = attemptId,
                        QuestionId = questionId,
                        OptionResponses = new List<TestQuestionOptionResponse>(),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    // Add selected options
                    response.OptionResponses = new List<TestQuestionOptionResponse>();
                    foreach (var optionId in selectedOptionIds)
                    {
                        response.OptionResponses.Add(new TestQuestionOptionResponse
                        {
                            Id = Guid.NewGuid(),
                            QuestionResponseId = response.Id,
                            OptionId = optionId,
                            IsSelected = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        });
                    }

                    // Evaluate if the response is correct
                    await EvaluateResponseInternalAsync(response);

                    return await _testQuestionResponseRepository.AddAsync(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving option responses");
                throw;
            }
        }

        public async Task<TestQuestionResponse> ProvideReviewerFeedbackAsync(Guid responseId, string feedback)
        {
            try
            {
                var response = await _testQuestionResponseRepository.GetByIdAsync(responseId);
                if (response == null)
                {
                    throw new KeyNotFoundException($"Test question response with ID {responseId} not found");
                }

                response.ReviewerFeedback = feedback;
                response.IsReviewed = true;
                response.ReviewedAt = DateTime.UtcNow;
                response.UpdatedAt = DateTime.UtcNow;

                return await _testQuestionResponseRepository.UpdateAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error providing reviewer feedback");
                throw;
            }
        }

        public async Task<TestQuestionResponse> EvaluateResponseAsync(Guid responseId)
        {
            try
            {
                var response = await _testQuestionResponseRepository.GetByIdAsync(responseId);
                if (response == null)
                {
                    throw new KeyNotFoundException($"Test question response with ID {responseId} not found");
                }

                await EvaluateResponseInternalAsync(response);
                return await _testQuestionResponseRepository.UpdateAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error evaluating test question response");
                throw;
            }
        }

        private async Task EvaluateResponseInternalAsync(TestQuestionResponse response)
        {
            // Get the question to determine its type and how to evaluate correctness
            var question = await _testQuestionRepository.GetByIdWithOptionsAsync(response.QuestionId);
            if (question == null)
            {
                throw new KeyNotFoundException($"Test question with ID {response.QuestionId} not found");
            }

            // Get correct options for the question
            var correctOptions = await _testQuestionOptionRepository.GetCorrectOptionsForQuestionAsync(question.Id);
            var correctOptionIds = correctOptions.Select(o => o.Id).ToList();

            // Get selected options for this response
            var selectedOptionIds = response.OptionResponses?.Select(o => o.OptionId).ToList() ?? new List<Guid>();

            // Evaluate based on question type
            switch (question.QuestionType)
            {
                case Domain.Enums.QuestionType.SingleChoice:
                    // For single choice, the response is correct if the only selected option is correct
                    response.IsCorrect = selectedOptionIds.Count == 1 && correctOptionIds.Contains(selectedOptionIds[0]);
                    break;

                case Domain.Enums.QuestionType.MultipleChoice:
                    // For multiple choice, all correct options must be selected and no incorrect options
                    response.IsCorrect = selectedOptionIds.Count > 0 && 
                                        correctOptionIds.Count == selectedOptionIds.Count && 
                                        correctOptionIds.All(id => selectedOptionIds.Contains(id));
                    break;

                // TrueFalse тип отсутствует в перечислении QuestionType
                // Удаляем дублирующий case

                case Domain.Enums.QuestionType.Text:
                case Domain.Enums.QuestionType.Code:
                    // For text-based answers, we'll need manual review
                    response.IsCorrect = null; // Requires manual grading
                    break;

                // Тип Matching отсутствует в перечислении QuestionType, поэтому удаляем этот case

                default:
                    response.IsCorrect = false;
                    break;
            }

            // For questions that require manual grading, set points earned to null
            if (response.IsCorrect == null)
            {
                response.PointsEarned = null;
            }
            else
            {
                // For automatically graded questions, set points earned based on correctness
                // IsCorrect здесь не null, но может быть bool или bool?
                response.PointsEarned = response.IsCorrect == true ? question.Points : 0;
            }
        }
    }
}