using Aplication.Order.Commands;
using Aplication.Order.Queries;
using Aplication.Order;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.WebUI.Controllers;

namespace Admin.Web.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            var orders = await Mediator.Send(new GetAllOrdersQuery());
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await Mediator.Send(new GetOrderByIdQuery { OrderId = id });
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderCommand command)
        {
            if (id != command.OrderId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await Mediator.Send(new DeleteOrderCommand { OrderId = id });
            return NoContent();
        }
    }
}
