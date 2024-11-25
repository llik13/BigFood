using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private IUnitOfWork _unitOfWork;

    public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        try
        {
            var results = await _unitOfWork._userRepository.GetAllAsync();
            _unitOfWork.Commit();
            _logger.LogInformation($"Returned all users from database.");
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting all users. {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }

    [HttpGet("GetById/{id}", Name = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        try
        {
            var result = await _unitOfWork._userRepository.GetAsync(id);
            _unitOfWork.Commit();
            if (result == null)
            {
                _logger.LogError($"User with id: {id}, is not found.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Returned user with id: {id}.");
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting user with id: {id}. Message: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }
}