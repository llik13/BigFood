using CourierService.Application.Orders.Queries.GetOrderDetails;
using CourierService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CourierService.Web.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrdersController : ControllerBase
{
    private IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetOrderById/{id}")]
    public async Task<ActionResult<OrderDetailsDTO>> GetOrderById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetOrderDetailsQuery {Id = id});
        return Ok(result);
    }
}
