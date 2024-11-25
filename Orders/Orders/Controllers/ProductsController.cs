using Microsoft.AspNetCore.Mvc;
using Orders.BLL.Services.Contrancts;
using Orders.DAL.Models;
using Orders.DAL.Pagination.Parameters;

namespace Orders.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    
    private IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllOrders([FromQuery] OrderParameters orderParameters)
    {
        var result = await _productService.GetAllProductsAsync();
        return Ok(result);
    }
}