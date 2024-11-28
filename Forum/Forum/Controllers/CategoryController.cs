using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;

    private IUnitOfWork _unitofWork;
    public CategoryController(ILogger<CategoryController> logger, IUnitOfWork unitofWork)
    {
        _logger = logger;
        _unitofWork = unitofWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoriesAsync()
    {
        try
        {
            var results = await _unitofWork._categoryRepository.GetAllAsync();
            _unitofWork.Commit();
            _logger.LogInformation($"Returned all categories from database.");
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Transaction Failed! Something went wrong inside GetCategoryAsync() action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }      
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Category>> GetByIdAsync(int id)
    {
        try
        {
            var result = await _unitofWork._categoryRepository.GetAsync(id);
            _unitofWork.Commit();
            if (result == null)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Returned category with id: {id}");
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside GetAsync action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> PostCategoryAsync([FromBody] Category newCategory)
    {
        try
        {
            if (newCategory == null)
            {
                _logger.LogError("Category object sent from client is null.");
                return BadRequest("Category object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Category object sent from client.");
                return BadRequest("Invalid model object");
            }
            var created_id = await _unitofWork._categoryRepository.AddAsync(newCategory);
            Console.WriteLine("in controller before commit");
            //var CreatedCategory = await _unitofWork._categoryRepository.GetAsync(created_id);
            _unitofWork.Commit();
            Console.WriteLine("in controller after commit");
            //return CreatedAtRoute("GetCategoryById", new { id = created_id}, CreatedCategory);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside PostCategoryAsync action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> PutAsync (int id, [FromBody] Category updateCategory)
    {
        try
        {
            if (updateCategory == null)
            {
                _logger.LogError("Category object sent from client is null.");
                return BadRequest("Category object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid category object sent from client.");
                return BadRequest("Invalid category object");
            }
            var CategoryEntity = await _unitofWork._categoryRepository.GetAsync(id);
            if (CategoryEntity == null)
            {
                _logger.LogError($"category with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            updateCategory.id = id;
            await _unitofWork._categoryRepository.ReplaceAsync(updateCategory);
            _unitofWork.Commit();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside PutAsync action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAsync (int id)
    {
        try
        {
            var CategoryEntity = await _unitofWork._categoryRepository.GetAsync(id);
            if (CategoryEntity == null)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            await _unitofWork._categoryRepository.DeleteAsync(id);
            _unitofWork.Commit();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside Catalog action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }   
}