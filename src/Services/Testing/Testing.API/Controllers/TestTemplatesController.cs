using Microsoft.AspNetCore.Mvc;
using Testing.Application.Services;
using Testing.Domain.Entities;

namespace Testing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestTemplatesController : ControllerBase
    {
        private readonly ITestTemplateService _testTemplateService;
        private readonly ILogger<TestTemplatesController> _logger;

        public TestTemplatesController(
            ITestTemplateService testTemplateService,
            ILogger<TestTemplatesController> logger)
        {
            _testTemplateService = testTemplateService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestTemplate>>> GetAll()
        {
            try
            {
                var testTemplates = await _testTemplateService.GetAllTestTemplatesAsync();
                return Ok(testTemplates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test templates");
                return StatusCode(500, "An error occurred while retrieving test templates");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestTemplate>> GetById(Guid id)
        {
            try
            {
                var testTemplate = await _testTemplateService.GetTestTemplateByIdAsync(id);
                if (testTemplate == null)
                {
                    return NotFound($"Test template with ID {id} not found");
                }

                return Ok(testTemplate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving test template");
                return StatusCode(500, "An error occurred while retrieving the test template");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TestTemplate>> Create(TestTemplate testTemplate)
        {
            try
            {
                var createdTemplate = await _testTemplateService.CreateTestTemplateAsync(testTemplate);
                return CreatedAtAction(nameof(GetById), new { id = createdTemplate.Id }, createdTemplate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test template");
                return StatusCode(500, "An error occurred while creating the test template");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TestTemplate testTemplate)
        {
            try
            {
                if (id != testTemplate.Id)
                {
                    return BadRequest("The ID in the URL does not match the ID in the request body");
                }

                var existingTemplate = await _testTemplateService.GetTestTemplateByIdAsync(id);
                if (existingTemplate == null)
                {
                    return NotFound($"Test template with ID {id} not found");
                }

                await _testTemplateService.UpdateTestTemplateAsync(testTemplate);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test template");
                return StatusCode(500, "An error occurred while updating the test template");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingTemplate = await _testTemplateService.GetTestTemplateByIdAsync(id);
                if (existingTemplate == null)
                {
                    return NotFound($"Test template with ID {id} not found");
                }

                await _testTemplateService.DeleteTestTemplateAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test template");
                return StatusCode(500, "An error occurred while deleting the test template");
            }
        }

        [HttpPost("{id}/publish")]
        public async Task<IActionResult> Publish(Guid id)
        {
            try
            {
                var existingTemplate = await _testTemplateService.GetTestTemplateByIdAsync(id);
                if (existingTemplate == null)
                {
                    return NotFound($"Test template with ID {id} not found");
                }

                await _testTemplateService.PublishTestTemplateAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing test template");
                return StatusCode(500, "An error occurred while publishing the test template");
            }
        }

        [HttpPost("{id}/unpublish")]
        public async Task<IActionResult> Unpublish(Guid id)
        {
            try
            {
                var existingTemplate = await _testTemplateService.GetTestTemplateByIdAsync(id);
                if (existingTemplate == null)
                {
                    return NotFound($"Test template with ID {id} not found");
                }

                await _testTemplateService.UnpublishTestTemplateAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unpublishing test template");
                return StatusCode(500, "An error occurred while unpublishing the test template");
            }
        }

        [HttpPost("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            try
            {
                var existingTemplate = await _testTemplateService.GetTestTemplateByIdAsync(id);
                if (existingTemplate == null)
                {
                    return NotFound($"Test template with ID {id} not found");
                }

                await _testTemplateService.ActivateTestTemplateAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating test template");
                return StatusCode(500, "An error occurred while activating the test template");
            }
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                var existingTemplate = await _testTemplateService.GetTestTemplateByIdAsync(id);
                if (existingTemplate == null)
                {
                    return NotFound($"Test template with ID {id} not found");
                }

                await _testTemplateService.DeactivateTestTemplateAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating test template");
                return StatusCode(500, "An error occurred while deactivating the test template");
            }
        }
    }
}