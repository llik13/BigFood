using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PostController : ControllerBase
{
    
    private readonly ILogger<PostController> _logger;
    
    private IUnitOfWork _unitofWork;
    
    public PostController(ILogger<PostController> logger, IUnitOfWork unitofWork)
    {
        _logger = logger;
        _unitofWork = unitofWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPostAsync()
    {
        try
        {
            var results = await _unitofWork._postRepository.GetAllAsync();
            _unitofWork.Commit();
            _logger.LogInformation($"Returned all posts from database.");
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
    public async Task<ActionResult<ResponsePost>> GetByIdAsync(int id)
    {
        try
        {
            var result = await _unitofWork._postRepository.GetAsync(id);
            _unitofWork.Commit();
            if (result == null)
            {
                _logger.LogError($"Post with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Returned post with id: {id}");
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
    public async Task<ActionResult> PostPostAsync([FromBody] Post newPost)
    {
        try
        {
            if (newPost == null)
            {
                _logger.LogError("Post object sent from client is null.");
                return BadRequest("Post object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid post object sent from client.");
                return BadRequest("Invalid model object");
            }
            var created_id = await _unitofWork._postRepository.AddAsync(newPost);
            //var CreatedPost = await _unitofWork._postRepository.GetAsync(created_id);
            _unitofWork.Commit();
            //return CreatedAtRoute("GetPostById", new { id = created_id}, CreatedPost);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside PostPostAsync action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
    
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> PutAsync (int id, [FromBody] Post updatePost)
    {
        try
        {
            if (updatePost == null)
            {
                _logger.LogError("Post object sent from client is null.");
                return BadRequest("Post object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid post object sent from client.");
                return BadRequest("Invalid post object");
            }
            var PostEntity = await _unitofWork._postRepository.GetAsync(id);
            if (PostEntity == null)
            {
                _logger.LogError($"post with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            await _unitofWork._postRepository.ReplaceAsync(updatePost);
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
            var PostEntity = await _unitofWork._postRepository.GetAsync(id);
            if (PostEntity == null)
            {
                _logger.LogError($"Post with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            await _unitofWork._postRepository.DeleteAsync(id);
            _unitofWork.Commit();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside Catalog action: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    //[HttpGet("GetAllInfoPosts")]
    //public async Task<ActionResult> GetAllInfoPostsAsync()
    //{
    //    try
    //    {
    //        var posts = await _unitofWork._postRepository.GetAllInfoAsync();
    //        _unitofWork.Commit();
    //        _logger.LogInformation($"Returned all posts from database.");
    //        return Ok(posts);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError($"Transaction Failed! Something went wrong inside GetAllAsync() action: {ex.Message}");
    //        return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
    //    }
    //}
}