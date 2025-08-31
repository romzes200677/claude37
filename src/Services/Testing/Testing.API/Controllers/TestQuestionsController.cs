using Microsoft.AspNetCore.Mvc;
using Testing.Application.Services;
using Testing.Domain.Entities;
using Testing.Domain.Enums;

namespace Testing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestQuestionsController : ControllerBase
    {
        private readonly ITestQuestionService _testQuestionService;
        private readonly ILogger<TestQuestionsController> _logger;

        public TestQuestionsController(
            ITestQuestionService testQuestionService,
            ILogger<TestQuestionsController> logger)
        {
            _testQuestionService = testQuestionService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestQuestion>> GetById(Guid id)
        {
            try
            {
                var question = await _testQuestionService.GetQuestionByIdAsync(id);
                if (question == null)
                {
                    return NotFound($"Question with ID {id} not found");
                }

                return Ok(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving question");
                return StatusCode(500, "An error occurred while retrieving the question");
            }
        }

        [HttpGet("byTemplate/{testTemplateId}")]
        public async Task<ActionResult<IEnumerable<TestQuestion>>> GetByTestTemplate(Guid testTemplateId)
        {
            try
            {
                var questions = await _testQuestionService.GetQuestionsByTestTemplateAsync(testTemplateId);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving questions by test template");
                return StatusCode(500, "An error occurred while retrieving questions");
            }
        }

        [HttpGet("byTemplate/{testTemplateId}/byType/{questionType}")]
        public async Task<ActionResult<IEnumerable<TestQuestion>>> GetByType(Guid testTemplateId, QuestionType questionType)
        {
            try
            {
                var questions = await _testQuestionService.GetQuestionsByTypeAsync(testTemplateId, questionType);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving questions by type");
                return StatusCode(500, "An error occurred while retrieving questions");
            }
        }

        [HttpGet("byTemplate/{testTemplateId}/byDifficulty/{difficultyLevel}")]
        public async Task<ActionResult<IEnumerable<TestQuestion>>> GetByDifficulty(Guid testTemplateId, DifficultyLevel difficultyLevel)
        {
            try
            {
                var questions = await _testQuestionService.GetQuestionsByDifficultyAsync(testTemplateId, difficultyLevel);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving questions by difficulty");
                return StatusCode(500, "An error occurred while retrieving questions");
            }
        }

        [HttpGet("byTemplate/{testTemplateId}/totalPoints")]
        public async Task<ActionResult<int>> GetTotalPoints(Guid testTemplateId)
        {
            try
            {
                var totalPoints = await _testQuestionService.GetTotalPointsForTestAsync(testTemplateId);
                return Ok(totalPoints);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating total points");
                return StatusCode(500, "An error occurred while calculating total points");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TestQuestion>> Create(TestQuestion question)
        {
            try
            {
                var createdQuestion = await _testQuestionService.CreateQuestionAsync(question);
                return CreatedAtAction(nameof(GetById), new { id = createdQuestion.Id }, createdQuestion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating question");
                return StatusCode(500, "An error occurred while creating the question");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TestQuestion question)
        {
            try
            {
                if (id != question.Id)
                {
                    return BadRequest("The ID in the URL does not match the ID in the request body");
                }

                var existingQuestion = await _testQuestionService.GetQuestionByIdAsync(id);
                if (existingQuestion == null)
                {
                    return NotFound($"Question with ID {id} not found");
                }

                await _testQuestionService.UpdateQuestionAsync(question);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating question");
                return StatusCode(500, "An error occurred while updating the question");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingQuestion = await _testQuestionService.GetQuestionByIdAsync(id);
                if (existingQuestion == null)
                {
                    return NotFound($"Question with ID {id} not found");
                }

                await _testQuestionService.DeleteQuestionAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting question");
                return StatusCode(500, "An error occurred while deleting the question");
            }
        }

        [HttpPost("reorder")]
        public async Task<IActionResult> ReorderQuestions(Guid testTemplateId, [FromBody] Dictionary<Guid, int> questionOrderMap)
        {
            try
            {
                await _testQuestionService.ReorderQuestionsAsync(testTemplateId, questionOrderMap);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reordering questions");
                return StatusCode(500, "An error occurred while reordering questions");
            }
        }
    }
}