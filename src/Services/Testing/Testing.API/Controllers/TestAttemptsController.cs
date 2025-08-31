using Microsoft.AspNetCore.Mvc;
using Testing.Application.Services;
using Testing.Domain.Entities;
using Testing.Domain.Enums;

namespace Testing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestAttemptsController : ControllerBase
    {
        private readonly ITestAttemptService _testAttemptService;
        private readonly ILogger<TestAttemptsController> _logger;

        public TestAttemptsController(
            ITestAttemptService testAttemptService,
            ILogger<TestAttemptsController> logger)
        {
            _testAttemptService = testAttemptService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestAttempt>> GetById(Guid id)
        {
            try
            {
                var attempt = await _testAttemptService.GetAttemptByIdAsync(id);
                if (attempt == null)
                {
                    return NotFound($"Test attempt with ID {id} not found");
                }

                return Ok(attempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test attempt");
                return StatusCode(500, "An error occurred while retrieving the test attempt");
            }
        }

        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<TestAttempt>>> GetByUser(Guid userId)
        {
            try
            {
                var attempts = await _testAttemptService.GetAttemptsByUserAsync(userId);
                return Ok(attempts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test attempts by user");
                return StatusCode(500, "An error occurred while retrieving test attempts");
            }
        }

        [HttpGet("byTemplate/{testTemplateId}")]
        public async Task<ActionResult<IEnumerable<TestAttempt>>> GetByTestTemplate(Guid testTemplateId)
        {
            try
            {
                var attempts = await _testAttemptService.GetAttemptsByTestTemplateAsync(testTemplateId);
                return Ok(attempts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test attempts by template");
                return StatusCode(500, "An error occurred while retrieving test attempts");
            }
        }

        [HttpGet("byUser/{userId}/byTemplate/{testTemplateId}")]
        public async Task<ActionResult<IEnumerable<TestAttempt>>> GetByUserAndTestTemplate(Guid userId, Guid testTemplateId)
        {
            try
            {
                var attempts = await _testAttemptService.GetAttemptsByUserAndTestTemplateAsync(userId, testTemplateId);
                return Ok(attempts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test attempts by user and template");
                return StatusCode(500, "An error occurred while retrieving test attempts");
            }
        }

        [HttpGet("byUser/{userId}/byTemplate/{testTemplateId}/latest")]
        public async Task<ActionResult<TestAttempt>> GetLatestAttempt(Guid userId, Guid testTemplateId)
        {
            try
            {
                var attempt = await _testAttemptService.GetLatestAttemptAsync(userId, testTemplateId);
                if (attempt == null)
                {
                    return NotFound($"No test attempts found for user {userId} and template {testTemplateId}");
                }

                return Ok(attempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving latest test attempt");
                return StatusCode(500, "An error occurred while retrieving the latest test attempt");
            }
        }

        [HttpGet("byUser/{userId}/byTemplate/{testTemplateId}/count")]
        public async Task<ActionResult<int>> GetAttemptCount(Guid userId, Guid testTemplateId)
        {
            try
            {
                var count = await _testAttemptService.GetAttemptCountAsync(userId, testTemplateId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test attempt count");
                return StatusCode(500, "An error occurred while retrieving test attempt count");
            }
        }

        [HttpGet("byUser/{userId}/byTemplate/{testTemplateId}/canStart")]
        public async Task<ActionResult<bool>> CanUserStartNewAttempt(Guid userId, Guid testTemplateId)
        {
            try
            {
                var canStart = await _testAttemptService.CanUserStartNewAttemptAsync(userId, testTemplateId);
                return Ok(canStart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if user can start new attempt");
                return StatusCode(500, "An error occurred while checking if user can start new attempt");
            }
        }

        [HttpPost("start")]
        public async Task<ActionResult<TestAttempt>> StartTestAttempt(Guid userId, Guid testTemplateId)
        {
            try
            {
                var attempt = await _testAttemptService.StartTestAttemptAsync(userId, testTemplateId);
                return CreatedAtAction(nameof(GetById), new { id = attempt.Id }, attempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting test attempt");
                return StatusCode(500, "An error occurred while starting the test attempt");
            }
        }

        [HttpPost("{attemptId}/submit")]
        public async Task<ActionResult<TestAttempt>> SubmitTestAttempt(Guid attemptId)
        {
            try
            {
                var attempt = await _testAttemptService.SubmitTestAttemptAsync(attemptId);
                return Ok(attempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting test attempt");
                return StatusCode(500, "An error occurred while submitting the test attempt");
            }
        }

        [HttpPost("{attemptId}/abandon")]
        public async Task<ActionResult<TestAttempt>> AbandonTestAttempt(Guid attemptId)
        {
            try
            {
                var attempt = await _testAttemptService.AbandonTestAttemptAsync(attemptId);
                return Ok(attempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error abandoning test attempt");
                return StatusCode(500, "An error occurred while abandoning the test attempt");
            }
        }

        [HttpPost("{attemptId}/updateScore")]
        public async Task<ActionResult<TestAttempt>> UpdateTestAttemptScore(Guid attemptId)
        {
            try
            {
                var attempt = await _testAttemptService.UpdateTestAttemptScoreAsync(attemptId);
                return Ok(attempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test attempt score");
                return StatusCode(500, "An error occurred while updating the test attempt score");
            }
        }

        [HttpPost("{attemptId}/review")]
        public async Task<ActionResult<TestAttempt>> ReviewTestAttempt(Guid attemptId, [FromBody] ReviewModel reviewModel)
        {
            try
            {
                var attempt = await _testAttemptService.ReviewTestAttemptAsync(attemptId, reviewModel.ReviewerId, reviewModel.ReviewNotes);
                return Ok(attempt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reviewing test attempt");
                return StatusCode(500, "An error occurred while reviewing the test attempt");
            }
        }

        [HttpGet("byStatus/{status}")]
        public async Task<ActionResult<IEnumerable<TestAttempt>>> GetByStatus(TestAttemptStatus status)
        {
            try
            {
                var attempts = await _testAttemptService.GetAttemptsByStatusAsync(status);
                return Ok(attempts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test attempts by status");
                return StatusCode(500, "An error occurred while retrieving test attempts");
            }
        }

        [HttpGet("byReviewStatus/{reviewStatus}")]
        public async Task<ActionResult<IEnumerable<TestAttempt>>> GetByReviewStatus(ReviewStatus reviewStatus)
        {
            try
            {
                var attempts = await _testAttemptService.GetAttemptsByReviewStatusAsync(reviewStatus);
                return Ok(attempts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test attempts by review status");
                return StatusCode(500, "An error occurred while retrieving test attempts");
            }
        }
    }

    public class ReviewModel
    {
        public Guid ReviewerId { get; set; }
        public string ReviewNotes { get; set; }
    }
}