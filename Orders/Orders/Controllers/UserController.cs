using Microsoft.AspNetCore.Mvc;
using Orders.BLL.Services.Contrancts;
using Orders.DAL.Models;
using Orders.DAL.Pagination.Parameters;

namespace Orders.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    
    private IUserService _userService;

    public UserController(ILogger<ProductsController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllOrders([FromQuery] OrderParameters orderParameters)
    {
        var result = await _userService.GetAllUsersAsync();
        return Ok(result);
    }
}