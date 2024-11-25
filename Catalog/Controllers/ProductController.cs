using Catalog.BuisnesDataLayer.DTO.Request;
using Catalog.BuisnesDataLayer.DTO.Response;
using Catalog.BuisnesDataLayer.Interfaces;
using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Pagination.Parametrs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Catalog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("full")]
        [ProducesResponseType(typeof(IEnumerable<ProductFullResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductFullResponseDTO>>> GetProductFullInformationAsync([FromQuery] ProductParametrs productParametrs)
        {
            var products = await productService.GetFullProductsInformationAsync(productParametrs);

            return Ok(products);
        }

        [HttpGet("summary")]
        [ProducesResponseType(typeof(IEnumerable<ProductSummaryResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductFullResponseDTO>>> GetProducSummaryInformationAsync([FromQuery] ProductParametrs productParametrs)
        {
            var products = await productService.GetProductSummaryInformationAsync(productParametrs);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddProductAsync([FromBody] ProductRequestDTO product)
        {
            await productService.AddProductAsync(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveProductAsync(int id)
        {
            await productService.RemoveProductAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateProductAsync(ProductRequestDTO product)
        {
            await productService.UpdateProductAsync(product);
            return Ok();
        }
    }
}
