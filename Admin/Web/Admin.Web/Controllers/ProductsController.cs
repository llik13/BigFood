using Aplication.Product;
using Aplication.Product.Commands;
using Aplication.Product.Queries;
using Domain.Entitites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.WebUI.Controllers;

namespace Admin.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Authorize(Roles = "admin")]
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, null);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var products = await Mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await Mediator.Send(new GetProductByIdQuery (id));
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.ProductId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await Mediator.Send(new DeleteProductCommand { ProductId = id });
            return NoContent();
        }

        [HttpPost("categories/{categoryId}")]
        public async Task<IActionResult> AddProductToCategory(int categoryId, [FromBody] Product product)
        {
            await Mediator.Send(new AddProductToCategoryCommand(categoryId, product));
            return NoContent();
        }

        [HttpDelete("categories/{categoryId}/{productId}")]
        public async Task<IActionResult> RemoveProductFromCategory(int categoryId, int productId)
        {
            await Mediator.Send(new RemoveProductFromCategoryCommand(categoryId, productId));
            return NoContent();
        }
    }

}
