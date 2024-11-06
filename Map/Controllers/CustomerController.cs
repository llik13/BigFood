using Map.Entities;
using Map.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Map.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        private IUnitOfWork _unitofWork;

        public CustomerController(ILogger<CustomerController> logger, IUnitOfWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }


        // GET: api/Product/GetAllProducts
        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<IEnumerable<Customers>>> GetAllProductsAsync()
        {
            try
            {
                var results = await _unitofWork._customerRepository.GetAllAsync();
                _unitofWork.Commit();
                _logger.LogInformation($"Returned all customers from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetAllCustomersAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // GET: api/Product/GetById/id
        [HttpGet("GetById/{id}", Name = "GetCustomerById")]
        public async Task<ActionResult<Customers>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitofWork._customerRepository.GetAsync(id);
                _unitofWork.Commit();
                if (result == null)
                {
                    _logger.LogError($"Customer with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned customer with id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIdAsync() action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // POST: api/Product
        [HttpPost("PostCustomer")]
        public async Task<ActionResult> PostProductAsync([FromBody] Customers newCustomer)
        {
            try
            {
                if (newCustomer == null)
                {
                    _logger.LogError("Customer object sent from client is null.");
                    return BadRequest("Customer object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Customer object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var created_id = await _unitofWork._customerRepository.AddAsync(newCustomer);
                var createdCustomer = await _unitofWork._customerRepository.GetAsync(created_id);
                _unitofWork.Commit();
                return CreatedAtRoute("GetCustomerById", new { id = created_id }, createdCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostCustomerAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Put/{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Customers updateProduct)
        {
            try
            {
                if (updateProduct == null)
                {
                    _logger.LogError("Customer object sent from client is null.");
                    return BadRequest("Customer object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid customer object sent from client.");
                    return BadRequest("Invalid customer object");
                }
                var ProductEntity = await _unitofWork._customerRepository.GetAsync(id);
                if (ProductEntity == null)
                {
                    _logger.LogError($"Customer with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitofWork._customerRepository.ReplaceAsync(updateProduct);
                _unitofWork.Commit();
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
                var ProductEntity = await _unitofWork._customerRepository.GetAsync(id);
                if (ProductEntity == null)
                {
                    _logger.LogError($"Customer with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitofWork._customerRepository.DeleteAsync(id);
                _unitofWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Product action: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
