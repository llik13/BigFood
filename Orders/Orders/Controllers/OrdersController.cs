using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Orders.BLL.DTO.Requests;
using Orders.BLL.DTO.Responses;
using Orders.BLL.Services.Contrancts;
using Orders.DAL.Models;
using Orders.DAL.Pagination.Parameters;
using Orders.DAL.Repositories;
using Orders.DAL.Specification;
using Orders.DAL.UOF;

namespace Orders.Controllers;

[ApiController]

[Route("/api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    
    private IOrderService _orderService;

    private IValidator<OrderRequest> _validator;

    public OrdersController(IValidator<OrderRequest> validator, ILogger<OrdersController> logger, IOrderService orderService)
    {
        _validator = validator;
        _logger = logger;
        _orderService = orderService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ShortOrderResponse>>> GetAllOrders([FromQuery] OrderParameters orderParameters)
    {
        var result = await _orderService.GetAllOrdersAsync(orderParameters);
        return Ok(result);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderResponse>> GetOrderById([FromRoute] int id)
    {
        var result = await _orderService.GetOrderByIdAsync(id);
        
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderResponse>> AddOrder([FromBody] OrderRequest orderRequest)
    {
        ValidationResult result = await _validator.ValidateAsync(orderRequest);

        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            return BadRequest(result.Errors);
        }
        
        return await _orderService.AddOrderAsync(orderRequest);
    }

    [HttpPut("Accept")]
    public async Task<ActionResult> AcceptOrder(int orderId)
    {
        _orderService.ChangeStatus(orderId, BLL.Enums.OrderStatus.Accepted);
        return Ok();
    }

    [HttpPut("Delivered")]
    public async Task<ActionResult> AcceptDeliverdOrder(int orderId)
    {
        _orderService.ChangeStatus(orderId, BLL.Enums.OrderStatus.Delivered);
        return Ok();
    }
    [HttpPut("Cancelled")]
    public async Task<ActionResult> CancelledOrder(int orderId)
    {
        _orderService.ChangeStatus(orderId, BLL.Enums.OrderStatus.Cancelled);
        return Ok();
    }

}
