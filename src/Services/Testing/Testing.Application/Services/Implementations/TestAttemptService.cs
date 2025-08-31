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
    public class TestAttemptService : ITestAttemptService
    {
        private readonly ITestAttemptRepository _testAttemptRepository;
        private readonly ITestTemplateRepository _testTemplateRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly ILogger<TestAttemptService> _logger;

        public TestAttemptService(
            ITestAttemptRepository testAttemptRepository,
            ITestTemplateRepository testTemplateRepository,
            ITestQuestionRepository testQuestionRepository,
            ILogger<TestAttemptService> logger)
        {
            _testAttemptRepository = testAttemptRepository;
            _testTemplateRepository = testTemplateRepository;
            _testQuestionRepository = testQuestionRepository;
            _logger = logger;
        }

        public async Task<TestAttempt> GetAttemptByIdAsync(Guid id)
        {
            return await _testAttemptRepository.GetByIdAsync(id);
        }

        // Этот метод не определен в интерфейсе ITestAttemptService, поэтому делаем его приватным
        private async Task<TestAttempt?> GetByIdWithResponsesAsync(Guid id)
        {
            return await _testAttemptRepository.GetByIdWithResponsesAsync(id);
        }

        public async Task<IEnumerable<TestAttempt>> GetAttemptsByUserAsync(Guid userId)
        {
            return await _testAttemptRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<TestAttempt>> GetAttemptsByTestTemplateAsync(Guid testTemplateId)
        {
            return await _testAttemptRepository.GetByTestTemplateIdAsync(testTemplateId);
        }

        public async Task<IEnumerable<TestAttempt>> GetAttemptsByUserAndTestTemplateAsync(Guid userId, Guid testTemplateId)
        {
            return await _testAttemptRepository.GetByUserAndTestTemplateAsync(userId, testTemplateId);
        }



        public async Task<int> GetAttemptCountAsync(Guid userId, Guid testTemplateId)
        {
            return await _testAttemptRepository.GetAttemptCountAsync(userId, testTemplateId);
        }

        public async Task<TestAttempt> GetLatestAttemptAsync(Guid userId, Guid testTemplateId)
        {
            var attempt = await _testAttemptRepository.GetLatestAttemptAsync(userId, testTemplateId);
            return attempt ?? throw new KeyNotFoundException($"No attempt found for user {userId} and test template {testTemplateId}");
        }

        // Эти методы не определены в интерфейсе ITestAttemptService, поэтому делаем их приватными
        private async Task<IEnumerable<TestAttempt>> GetCompletedAttemptsAsync(Guid userId, Guid testTemplateId)
        {
            return await _testAttemptRepository.GetCompletedAttemptsAsync(userId, testTemplateId);
        }

        private async Task<IEnumerable<TestAttempt>> GetPassedAttemptsAsync(Guid userId, Guid testTemplateId)
        {
            return await _testAttemptRepository.GetPassedAttemptsAsync(userId, testTemplateId);
        }

        public async Task<TestAttempt> StartTestAttemptAsync(Guid userId, Guid testTemplateId)
        {
            try
            {
                var testTemplate = await _testTemplateRepository.GetByIdAsync(testTemplateId);
                if (testTemplate == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {testTemplateId} not found");
                }

                if (!testTemplate.IsActive)
                {
                    throw new InvalidOperationException("Cannot start an attempt for an inactive test template");
                }

                var testAttempt = new TestAttempt
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    TestTemplateId = testTemplateId,
                    StartedAt = DateTime.UtcNow,
                    Status = TestAttemptStatus.InProgress,
                    ReviewStatus = ReviewStatus.NotReviewed,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                return await _testAttemptRepository.AddAsync(testAttempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting test attempt");
                throw;
            }
        }

        public async Task<TestAttempt> SubmitTestAttemptAsync(Guid attemptId)
        {
            try
            {
                var testAttempt = await _testAttemptRepository.GetByIdWithResponsesAsync(attemptId);
                if (testAttempt == null)
                {
                    throw new KeyNotFoundException($"Test attempt with ID {attemptId} not found");
                }

                if (testAttempt.Status == TestAttemptStatus.Completed)
                {
                    throw new InvalidOperationException("Test attempt is already completed");
                }

                testAttempt.CompletedAt = DateTime.UtcNow;
                testAttempt.Status = TestAttemptStatus.Completed;
                testAttempt.UpdatedAt = DateTime.UtcNow;

                // Calculate score
                var totalPoints = await _testQuestionRepository.GetTotalPointsForTestAsync(testAttempt.TestTemplateId);
                var testTemplate = await _testTemplateRepository.GetByIdAsync(testAttempt.TestTemplateId);
                
                if (testTemplate != null && totalPoints > 0)
                {
                    // Calculate score based on correct responses
                    // This is a simplified calculation and should be enhanced based on specific requirements
                    int earnedPoints = 0;
                    // Logic to calculate earned points based on responses
                    // This would typically involve checking each response against correct answers
                    
                    testAttempt.Score = (int)((decimal)earnedPoints / totalPoints * 100);
                    testAttempt.IsPassed = testAttempt.Score >= testTemplate.PassingScore;
                }

                return await _testAttemptRepository.UpdateAsync(testAttempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing test attempt");
                throw;
            }
        }

        public async Task<TestAttempt> ReviewTestAttemptAsync(Guid attemptId, Guid reviewerId, string reviewNotes)
        {
            try
            {
                var testAttempt = await _testAttemptRepository.GetByIdAsync(attemptId);
                if (testAttempt == null)
                {
                    throw new KeyNotFoundException($"Test attempt with ID {attemptId} not found");
                }

                testAttempt.ReviewStatus = ReviewStatus.Reviewed;
                testAttempt.ReviewerId = reviewerId;
                testAttempt.ReviewNotes = reviewNotes;
                testAttempt.ReviewedAt = DateTime.UtcNow;
                testAttempt.UpdatedAt = DateTime.UtcNow;

                return await _testAttemptRepository.UpdateAsync(testAttempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reviewing test attempt");
                throw;
            }
        }

        public async Task<TestAttempt> UpdateTestAttemptScoreAsync(Guid attemptId)
        {
            try
            {
                var testAttempt = await _testAttemptRepository.GetByIdWithResponsesAsync(attemptId);
                if (testAttempt == null)
                {
                    throw new KeyNotFoundException($"Test attempt with ID {attemptId} not found");
                }

                var testTemplate = await _testTemplateRepository.GetByIdAsync(testAttempt.TestTemplateId);
                if (testTemplate == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {testAttempt.TestTemplateId} not found");
                }

                // Calculate score based on responses
                var totalPoints = await _testQuestionRepository.GetTotalPointsForTestAsync(testAttempt.TestTemplateId);
                if (totalPoints > 0)
                {
                    // Logic to calculate earned points based on responses
                    // This would typically involve checking each response against correct answers
                    int earnedPoints = 0;
                    // Add calculation logic here
                    
                    testAttempt.Score = (int)((decimal)earnedPoints / totalPoints * 100);
                    testAttempt.IsPassed = testAttempt.Score >= testTemplate.PassingScore;
                    testAttempt.UpdatedAt = DateTime.UtcNow;
                }

                return await _testAttemptRepository.UpdateAsync(testAttempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test attempt score");
                throw;
            }
        }

        public async Task<TestAttempt> AbandonTestAttemptAsync(Guid attemptId)
        {
            try
            {
                var testAttempt = await _testAttemptRepository.GetByIdAsync(attemptId);
                if (testAttempt == null)
                {
                    throw new KeyNotFoundException($"Test attempt with ID {attemptId} not found");
                }

                if (testAttempt.Status == TestAttemptStatus.Completed)
                {
                    throw new InvalidOperationException("Cannot abandon a completed test attempt");
                }

                testAttempt.Status = TestAttemptStatus.Abandoned;
                testAttempt.CompletedAt = DateTime.UtcNow;
                testAttempt.UpdatedAt = DateTime.UtcNow;

                return await _testAttemptRepository.UpdateAsync(testAttempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error abandoning test attempt");
                throw;
            }
        }

        public async Task<IEnumerable<TestAttempt>> GetAttemptsByStatusAsync(TestAttemptStatus status)
        {
            try
            {
                return await _testAttemptRepository.GetByStatusAsync(status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting test attempts by status");
                throw;
            }
        }

        public async Task<IEnumerable<TestAttempt>> GetAttemptsByReviewStatusAsync(ReviewStatus reviewStatus)
        {
            try
            {
                return await _testAttemptRepository.GetByReviewStatusAsync(reviewStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting test attempts by review status");
                throw;
            }
        }

        public async Task<bool> CanUserStartNewAttemptAsync(Guid userId, Guid testTemplateId)
        {
            try
            {
                var testTemplate = await _testTemplateRepository.GetByIdAsync(testTemplateId);
                if (testTemplate == null)
                {
                    throw new KeyNotFoundException($"Test template with ID {testTemplateId} not found");
                }

                if (!testTemplate.IsActive)
                {
                    return false;
                }

                // Check if user has reached the maximum number of attempts
                if (testTemplate.MaxAttempts > 0)
                {
                    var attemptCount = await GetAttemptCountAsync(userId, testTemplateId);
                    if (attemptCount >= testTemplate.MaxAttempts)
                    {
                        return false;
                    }
                }

                // Check if user has a passing attempt and max attempts is 1 (retakes not allowed)
                if (testTemplate.MaxAttempts == 1)
                {
                    var passedAttempts = await GetPassedAttemptsAsync(userId, testTemplateId);
                    if (passedAttempts.Any())
                    {
                        return false;
                    }
                }

                // Check if user has an in-progress attempt
                var allInProgressAttempts = await _testAttemptRepository.GetByStatusAsync(TestAttemptStatus.InProgress);
                var userInProgressAttempts = allInProgressAttempts.Where(a => a.UserId == userId && a.TestTemplateId == testTemplateId);
                if (userInProgressAttempts.Any())
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if user can start new attempt");
                throw;
            }
        }
    }
}