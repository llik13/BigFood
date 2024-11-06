using Map.Entities;
using Map.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Map.Controllers
{
    [Route("/api[controller]")]
    [ApiController]
    public class DeliverController : Controller
    {
        private readonly ILogger<DeliverController> _logger;
        public IUnitOfWork _unitOfWork { get; set; }

        public DeliverController(ILogger<DeliverController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllDelivers")]
        public async Task<ActionResult<IEnumerable<Deliver>>> GetAllDeliveriesAsync()
        {
            try
            {
                var results = await _unitOfWork._deliverRepository.GetAllAsync();
                _unitOfWork.Commit();
                _logger.LogInformation($"Returned all delivers from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetAllDeliversAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}", Name = "GetDeliverById")]
        public async Task<ActionResult<Deliver>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork._deliverRepository.GetAsync(id);
                _unitOfWork.Commit();
                if (result == null)
                {
                    _logger.LogError($"Product with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned deliver with id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIdAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost("PostDelivers")]
        public async Task<ActionResult> PostDeliveryAsync([FromBody] Deliver newDeliver)
        {
            try
            {
                if (newDeliver == null)
                {
                    _logger.LogError("Deliver object sent from client is null");
                    return BadRequest("Deliver object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Deliver object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var created_id = await _unitOfWork._deliverRepository.AddAsync(newDeliver);
                var createdDeliver = await _unitOfWork._deliverRepository.GetAsync(created_id);
                _unitOfWork.Commit();
                return CreatedAtRoute("GetDeliverById", new { id = created_id }, createdDeliver);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostDeliverAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("Put/{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Deliver updateDeliver)
        {
            try
            {
                if (updateDeliver == null)
                {
                    _logger.LogError("Deliver object sent from client is null.");
                    return BadRequest("Deliver object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Deliver object sent from client.");
                    return BadRequest("Invalid Deliver object");
                }
                var deliveryEntity = await _unitOfWork._deliverRepository.GetAsync(id);
                if (deliveryEntity == null)
                {
                    _logger.LogError($"Deliver with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitOfWork._deliverRepository.ReplaceAsync(updateDeliver);
                _unitOfWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PutAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var deliveryEntity = await _unitOfWork._deliveryRepository.GetAsync(id);
                if (deliveryEntity == null)
                {
                    _logger.LogError($"Deliver with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitOfWork._deliverRepository.DeleteAsync(id);
                _unitOfWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Deliver action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
