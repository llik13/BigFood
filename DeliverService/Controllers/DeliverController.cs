using Deliver.DAL.Entities;
using Deliver.DAL.Interfaces;
using Deliver.DAL.Migrations;
using DeliverService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliverService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegistrationService _registrationService;

        public DeliverController(IUnitOfWork unitOfWork, IRegistrationService registrationService)
        {
            _unitOfWork = unitOfWork;
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<ActionResult> addDeliverAsync([FromBody] DeliverModel deliver)
        {
            var result = _registrationService.GetDeliverById(deliver.CourierId);
            if (result == null)
                return BadRequest();
            await _unitOfWork._deliverRepository.AddAsync(deliver);
            return Ok();
        }
    }
}
