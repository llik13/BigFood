using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;

    private IUnitOfWork _unitofWork;
    public CommentController(ILogger<CommentController> logger, IUnitOfWork unitofWork)
    {
        _logger = logger;
        _unitofWork = unitofWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAllCommentsAsync()
    {
        try
        {
            var results = await _unitofWork._commentRepository.GetAllAsync();
            _unitofWork.Commit();
            _logger.LogInformation($"Returned all comments from database.");
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Transaction Failed! Something went wrong inside GetAllAsync() action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }      
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Comment>> GetByIdAsync(int id)
    {
        try
        {
            var result = await _unitofWork._commentRepository.GetAsync(id);
            _unitofWork.Commit();
            if (result == null)
            {
                _logger.LogError($"Comment with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Returned comment with id: {id}");
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
    public async Task<ActionResult> PostCommentAsync([FromBody] Comment newComment)
    {
        try
        {
            if (newComment == null)
            {
                _logger.LogError("Comment object sent from client is null.");
                return BadRequest("Comment object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Comment object sent from client.");
                return BadRequest("Invalid model object");
            }
            var created_id = await _unitofWork._commentRepository.AddAsync(newComment);
            Console.WriteLine("in controller before commit");
            //var CreatedComment = await _unitofWork._commentRepository.GetAsync(created_id);
            _unitofWork.Commit();
            Console.WriteLine("in controller after commit");
            //return CreatedAtRoute("GetCommentById", new { id = created_id}, CreatedComment);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside PostCommentAsync action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> PutAsync (int id, [FromBody] Comment updateComment)
    {
        try
        {
            if (updateComment == null)
            {
                _logger.LogError("Comment object sent from client is null.");
                return BadRequest("Comment object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid comment object sent from client.");
                return BadRequest("Invalid comment object");
            }
            var CommentEntity = await _unitofWork._commentRepository.GetAsync(id);
            if (CommentEntity == null)
            {
                _logger.LogError($"comment with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            await _unitofWork._commentRepository.ReplaceAsync(updateComment);
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
            var CommentEntity = await _unitofWork._commentRepository.GetAsync(id);
            if (CommentEntity == null)
            {
                _logger.LogError($"Comment with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            await _unitofWork._commentRepository.DeleteAsync(id);
            _unitofWork.Commit();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside Catalog action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult> GetCommentsAsync(int id)
    {
        try
        {
            var user = await _unitofWork._userRepository.GetAsync(id);
            if (user == null)
            {
                _logger.LogError($"User with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            var result = await _unitofWork._commentRepository.GetByUserId(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside Catalog action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
    
}