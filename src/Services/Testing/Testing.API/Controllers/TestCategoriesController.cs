using Microsoft.AspNetCore.Mvc;
using Testing.Application.Services;
using Testing.Domain.Entities;

namespace Testing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestCategoriesController : ControllerBase
    {
        private readonly ITestCategoryService _testCategoryService;
        private readonly ILogger<TestCategoriesController> _logger;

        public TestCategoriesController(
            ITestCategoryService testCategoryService,
            ILogger<TestCategoriesController> logger)
        {
            _testCategoryService = testCategoryService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestCategory>> GetById(Guid id)
        {
            try
            {
                var category = await _testCategoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound($"Category with ID {id} not found");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category");
                return StatusCode(500, "An error occurred while retrieving the category");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestCategory>>> GetAll()
        {
            try
            {
                var categories = await _testCategoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categories");
                return StatusCode(500, "An error occurred while retrieving categories");
            }
        }

        [HttpGet("root")]
        public async Task<ActionResult<IEnumerable<TestCategory>>> GetRootCategories()
        {
            try
            {
                var categories = await _testCategoryService.GetRootCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving root categories");
                return StatusCode(500, "An error occurred while retrieving root categories");
            }
        }

        [HttpGet("{parentId}/children")]
        public async Task<ActionResult<IEnumerable<TestCategory>>> GetChildCategories(Guid parentId)
        {
            try
            {
                var categories = await _testCategoryService.GetChildCategoriesAsync(parentId);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving child categories");
                return StatusCode(500, "An error occurred while retrieving child categories");
            }
        }

        [HttpGet("{categoryId}/path")]
        public async Task<ActionResult<IEnumerable<TestCategory>>> GetCategoryPath(Guid categoryId)
        {
            try
            {
                var categoryPath = await _testCategoryService.GetCategoryPathAsync(categoryId);
                return Ok(categoryPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category path");
                return StatusCode(500, "An error occurred while retrieving category path");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TestCategory>> Create(TestCategory category)
        {
            try
            {
                var createdCategory = await _testCategoryService.CreateCategoryAsync(category);
                return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                return StatusCode(500, "An error occurred while creating the category");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TestCategory category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest("The ID in the URL does not match the ID in the request body");
                }

                var existingCategory = await _testCategoryService.GetCategoryByIdAsync(id);
                if (existingCategory == null)
                {
                    return NotFound($"Category with ID {id} not found");
                }

                await _testCategoryService.UpdateCategoryAsync(category);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category");
                return StatusCode(500, "An error occurred while updating the category");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingCategory = await _testCategoryService.GetCategoryByIdAsync(id);
                if (existingCategory == null)
                {
                    return NotFound($"Category with ID {id} not found");
                }

                await _testCategoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category");
                return StatusCode(500, "An error occurred while deleting the category");
            }
        }

        [HttpGet("{categoryId}/templates")]
        public async Task<ActionResult<IEnumerable<TestTemplate>>> GetTemplatesByCategory(Guid categoryId, [FromQuery] bool includeChildCategories = false)
        {
            try
            {
                var templates = await _testCategoryService.GetTemplatesByCategoryAsync(categoryId, includeChildCategories);
                return Ok(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving templates by category");
                return StatusCode(500, "An error occurred while retrieving templates");
            }
        }
    }
}