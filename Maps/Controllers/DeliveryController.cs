using Map.Entities;
using Map.Repositories;
using Map.Repositories.Interfaces;
using Maps.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Map.Controllers
{
    [Route("/api[controller]")]
    [ApiController]
    public class DeliveryController : Controller
    {
        private readonly ILogger<DeliveryController> _logger;
        public IUnitOfWork _unitOfWork { get; set; }

        public DeliveryController(ILogger<DeliveryController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllDeliveries")]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetAllDeliveriesAsync()
        {
            try
            {
                var results = await _unitOfWork._deliveryRepository.GetAllAsync();
                _unitOfWork.Commit();
                _logger.LogInformation($"Returned all deliveries from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetAllDeliveriesAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}", Name = "GetDeliveryById")]
        public async Task<ActionResult<Delivery>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork._deliveryRepository.GetAsync(id);
                _unitOfWork.Commit();
                if(result == null)
                {
                    _logger.LogError($"Delivery with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned delivery with id: {id}");
                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIdAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("GetByStatus")]
        public async Task<ActionResult<Delivery>> GetByStatusAsync(string status)
        {
            try
            {
                var result = await _unitOfWork._deliveryRepository.GetByStatusAsync(status);
                _unitOfWork.Commit();
                if (result == null || result.Count() == 0)
                {
                    _logger.LogError($"Delivery with status: {status}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned delivery with status: {status}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByStatusAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("GetDeliveryWithDeliver")]
        public async Task<ActionResult<IEnumerable<DeliveryWithDeliverDTO>>> GetWithDeliverAsync()
        {
            try
            {
                var results = await _unitOfWork._deliveryRepository.GetDeliveriesAndDlivers();
                _unitOfWork.Commit();
                _logger.LogInformation($"Returned all deliveries with delivers information from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetWithDeliverAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        } 

        [HttpPost("PostDelivery")]
        public async Task<ActionResult> PostDeliveryAsync([FromBody] Delivery newDelivery) 
        {
            try
            {
                if(newDelivery == null)
                {
                    _logger.LogError("Delivery object sent from client is null");
                    return BadRequest("Delivery object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Delivery object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var created_id = await _unitOfWork._deliveryRepository.AddAsync(newDelivery);
                var createdDelivery = await _unitOfWork._deliveryRepository.GetAsync(created_id);
                _unitOfWork.Commit();
                return CreatedAtRoute("GetDeliveryById", new {id = created_id}, createdDelivery);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostProductAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("Put/{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Delivery updateDelivery)
        {
            try
            {
                if (updateDelivery == null)
                {
                    _logger.LogError("Delivery object sent from client is null.");
                    return BadRequest("Delivery object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Delivery object sent from client.");
                    return BadRequest("Invalid Delivery object");
                }
                var deliveryEntity = await _unitOfWork._deliveryRepository.GetAsync(id);
                if (deliveryEntity == null)
                {
                    _logger.LogError($"Delivery with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitOfWork._deliveryRepository.ReplaceAsync(updateDelivery);
                _unitOfWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PutAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("SetStatus/{id}")]
        public async Task<ActionResult> SetStatusAsync(int id, string status)
        {
            try
            {
                await _unitOfWork._deliveryRepository.SetStatusAsync(id, status);
                _unitOfWork.Commit();
                _logger.LogInformation($"Set status to database.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside SetStatusAsync() action: {ex.Message}");
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
                    _logger.LogError($"Delivery with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitOfWork._deliveryRepository.DeleteAsync(id);
                _unitOfWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delivery action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
