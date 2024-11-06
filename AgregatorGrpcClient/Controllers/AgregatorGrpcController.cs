using AgregatorGrpc.Protos;
using AgregatorGrpcClient.Protos;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgregatorGrpcClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgregatorGrpcController : ControllerBase
    {
        private readonly Protos.Agregator.AgregatorClient _client;

        public AgregatorGrpcController(Protos.Agregator.AgregatorClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protos.FullProductModel>>> GetProducts()
        {
            var fullProducts = await _client.GetAllAsync(new Empty());
            return Ok(fullProducts);
        }

    }
}
