using Microsoft.AspNetCore.Mvc;
using Testing.Application.Services;
using Testing.Domain.Entities;

namespace Testing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestQuestionOptionsController : ControllerBase
    {
        private readonly ITestQuestionOptionService _testQuestionOptionService;
        private readonly ILogger<TestQuestionOptionsController> _logger;

        public TestQuestionOptionsController(
            ITestQuestionOptionService testQuestionOptionService,
            ILogger<TestQuestionOptionsController> logger)
        {
            _testQuestionOptionService = testQuestionOptionService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestQuestionOption>> GetById(Guid id)
        {
            try
            {
                var option = await _testQuestionOptionService.GetOptionByIdAsync(id);
                if (option == null)
                {
                    return NotFound($"Option with ID {id} not found");
                }

                return Ok(option);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving question option");
                return StatusCode(500, "An error occurred while retrieving the question option");
            }
        }

        [HttpGet("byQuestion/{questionId}")]
        public async Task<ActionResult<IEnumerable<TestQuestionOption>>> GetByQuestion(Guid questionId)
        {
            try
            {
                var options = await _testQuestionOptionService.GetOptionsByQuestionAsync(questionId);
                return Ok(options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving question options");
                return StatusCode(500, "An error occurred while retrieving question options");
            }
        }

        [HttpGet("byQuestion/{questionId}/correct")]
        public async Task<ActionResult<IEnumerable<TestQuestionOption>>> GetCorrectOptions(Guid questionId)
        {
            try
            {
                var options = await _testQuestionOptionService.GetCorrectOptionsForQuestionAsync(questionId);
                return Ok(options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving correct question options");
                return StatusCode(500, "An error occurred while retrieving correct question options");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TestQuestionOption>> Create(TestQuestionOption option)
        {
            try
            {
                var createdOption = await _testQuestionOptionService.CreateOptionAsync(option);
                return CreatedAtAction(nameof(GetById), new { id = createdOption.Id }, createdOption);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating question option");
                return StatusCode(500, "An error occurred while creating the question option");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TestQuestionOption option)
        {
            try
            {
                if (id != option.Id)
                {
                    return BadRequest("The ID in the URL does not match the ID in the request body");
                }

                var existingOption = await _testQuestionOptionService.GetOptionByIdAsync(id);
                if (existingOption == null)
                {
                    return NotFound($"Option with ID {id} not found");
                }

                await _testQuestionOptionService.UpdateOptionAsync(option);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating question option");
                return StatusCode(500, "An error occurred while updating the question option");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingOption = await _testQuestionOptionService.GetOptionByIdAsync(id);
                if (existingOption == null)
                {
                    return NotFound($"Option with ID {id} not found");
                }

                await _testQuestionOptionService.DeleteOptionAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting question option");
                return StatusCode(500, "An error occurred while deleting the question option");
            }
        }

        [HttpPost("reorder")]
        public async Task<IActionResult> ReorderOptions(Guid questionId, [FromBody] Dictionary<Guid, int> optionOrderMap)
        {
            try
            {
                await _testQuestionOptionService.ReorderOptionsAsync(questionId, optionOrderMap);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reordering question options");
                return StatusCode(500, "An error occurred while reordering question options");
            }
        }
    }
}