using Microsoft.AspNetCore.Mvc;
using Testing.Application.Services;
using Testing.Domain.Entities;

namespace Testing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestQuestionResponsesController : ControllerBase
    {
        private readonly ITestQuestionResponseService _testQuestionResponseService;
        private readonly ILogger<TestQuestionResponsesController> _logger;

        public TestQuestionResponsesController(
            ITestQuestionResponseService testQuestionResponseService,
            ILogger<TestQuestionResponsesController> logger)
        {
            _testQuestionResponseService = testQuestionResponseService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestQuestionResponse>> GetById(Guid id)
        {
            try
            {
                var response = await _testQuestionResponseService.GetResponseByIdAsync(id);
                if (response == null)
                {
                    return NotFound($"Question response with ID {id} not found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving question response");
                return StatusCode(500, "An error occurred while retrieving the question response");
            }
        }

        [HttpGet("byAttempt/{attemptId}")]
        public async Task<ActionResult<IEnumerable<TestQuestionResponse>>> GetByAttempt(Guid attemptId)
        {
            try
            {
                var responses = await _testQuestionResponseService.GetResponsesByAttemptAsync(attemptId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving question responses by attempt");
                return StatusCode(500, "An error occurred while retrieving question responses");
            }
        }

        [HttpGet("byAttempt/{attemptId}/byQuestion/{questionId}")]
        public async Task<ActionResult<TestQuestionResponse>> GetByAttemptAndQuestion(Guid attemptId, Guid questionId)
        {
            try
            {
                var response = await _testQuestionResponseService.GetResponseByAttemptAndQuestionAsync(attemptId, questionId);
                if (response == null)
                {
                    return NotFound($"No response found for attempt {attemptId} and question {questionId}");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving question response by attempt and question");
                return StatusCode(500, "An error occurred while retrieving the question response");
            }
        }

        [HttpPost("saveText")]
        public async Task<ActionResult<TestQuestionResponse>> SaveTextResponse([FromBody] TextResponseModel model)
        {
            try
            {
                var response = await _testQuestionResponseService.SaveTextResponseAsync(
                    model.AttemptId, model.QuestionId, model.ResponseText);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving text response");
                return StatusCode(500, "An error occurred while saving the text response");
            }
        }

        [HttpPost("saveOptions")]
        public async Task<ActionResult<TestQuestionResponse>> SaveOptionResponses([FromBody] OptionResponseModel model)
        {
            try
            {
                var response = await _testQuestionResponseService.SaveOptionResponsesAsync(
                    model.AttemptId, model.QuestionId, model.SelectedOptionIds);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving option responses");
                return StatusCode(500, "An error occurred while saving the option responses");
            }
        }

        [HttpPost("{responseId}/evaluate")]
        public async Task<ActionResult<TestQuestionResponse>> EvaluateResponse(Guid responseId)
        {
            try
            {
                var response = await _testQuestionResponseService.EvaluateResponseAsync(responseId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error evaluating response");
                return StatusCode(500, "An error occurred while evaluating the response");
            }
        }

        [HttpPost("{responseId}/feedback")]
        public async Task<ActionResult<TestQuestionResponse>> ProvideReviewerFeedback(Guid responseId, [FromBody] FeedbackModel model)
        {
            try
            {
                var response = await _testQuestionResponseService.ProvideReviewerFeedbackAsync(responseId, model.Feedback);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error providing reviewer feedback");
                return StatusCode(500, "An error occurred while providing reviewer feedback");
            }
        }

        [HttpGet("byAttempt/{attemptId}/correct")]
        public async Task<ActionResult<IEnumerable<TestQuestionResponse>>> GetCorrectResponses(Guid attemptId)
        {
            try
            {
                var responses = await _testQuestionResponseService.GetCorrectResponsesAsync(attemptId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving correct responses");
                return StatusCode(500, "An error occurred while retrieving correct responses");
            }
        }

        [HttpGet("byAttempt/{attemptId}/incorrect")]
        public async Task<ActionResult<IEnumerable<TestQuestionResponse>>> GetIncorrectResponses(Guid attemptId)
        {
            try
            {
                var responses = await _testQuestionResponseService.GetIncorrectResponsesAsync(attemptId);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving incorrect responses");
                return StatusCode(500, "An error occurred while retrieving incorrect responses");
            }
        }
    }

    public class TextResponseModel
    {
        public Guid AttemptId { get; set; }
        public Guid QuestionId { get; set; }
        public string ResponseText { get; set; }
    }

    public class OptionResponseModel
    {
        public Guid AttemptId { get; set; }
        public Guid QuestionId { get; set; }
        public IEnumerable<Guid> SelectedOptionIds { get; set; }
    }

    public class FeedbackModel
    {
        public string Feedback { get; set; }
    }
}