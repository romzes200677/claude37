using Microsoft.AspNetCore.Mvc;
using Testing.Application.Services;
using Testing.Domain.Entities;

namespace Testing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestTagsController : ControllerBase
    {
        private readonly ITestTagService _testTagService;
        private readonly ILogger<TestTagsController> _logger;

        public TestTagsController(
            ITestTagService testTagService,
            ILogger<TestTagsController> logger)
        {
            _testTagService = testTagService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestTag>> GetById(Guid id)
        {
            try
            {
                var tag = await _testTagService.GetTagByIdAsync(id);
                if (tag == null)
                {
                    return NotFound($"Tag with ID {id} not found");
                }

                return Ok(tag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tag");
                return StatusCode(500, "An error occurred while retrieving the tag");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestTag>>> GetAll()
        {
            try
            {
                var tags = await _testTagService.GetAllTagsAsync();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tags");
                return StatusCode(500, "An error occurred while retrieving tags");
            }
        }

        [HttpGet("byName/{name}")]
        public async Task<ActionResult<IEnumerable<TestTag>>> GetByName(string name)
        {
            try
            {
                var tags = await _testTagService.GetTagsByNameAsync(name);
                return Ok(tags);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tags by name");
                return StatusCode(500, "An error occurred while retrieving tags");
            }
        }

        [HttpGet("byTemplate/{testTemplateId}")]
        public async Task<ActionResult<IEnumerable<TestTag>>> GetByTestTemplate(Guid testTemplateId)
        {
            try
            {
                var tags = await _testTagService.GetTagsByTestTemplateAsync(testTemplateId);
                return Ok(tags);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tags by test template");
                return StatusCode(500, "An error occurred while retrieving tags");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TestTag>> Create(TestTag tag)
        {
            try
            {
                var createdTag = await _testTagService.CreateTagAsync(tag);
                return CreatedAtAction(nameof(GetById), new { id = createdTag.Id }, createdTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating tag");
                return StatusCode(500, "An error occurred while creating the tag");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TestTag tag)
        {
            try
            {
                if (id != tag.Id)
                {
                    return BadRequest("The ID in the URL does not match the ID in the request body");
                }

                var existingTag = await _testTagService.GetTagByIdAsync(id);
                if (existingTag == null)
                {
                    return NotFound($"Tag with ID {id} not found");
                }

                await _testTagService.UpdateTagAsync(tag);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tag");
                return StatusCode(500, "An error occurred while updating the tag");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingTag = await _testTagService.GetTagByIdAsync(id);
                if (existingTag == null)
                {
                    return NotFound($"Tag with ID {id} not found");
                }

                await _testTagService.DeleteTagAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting tag");
                return StatusCode(500, "An error occurred while deleting the tag");
            }
        }

        [HttpGet("{tagId}/templates")]
        public async Task<ActionResult<IEnumerable<TestTemplate>>> GetTemplatesByTag(Guid tagId)
        {
            try
            {
                var templates = await _testTagService.GetTemplatesByTagAsync(tagId);
                return Ok(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving templates by tag");
                return StatusCode(500, "An error occurred while retrieving templates");
            }
        }
    }
}