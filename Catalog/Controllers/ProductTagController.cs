using Catalog.BuisnesDataLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTagController : ControllerBase
    {
        private readonly IProductTagService productTagService;

        public ProductTagController(IProductTagService productTagService)
        {
            this.productTagService = productTagService;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddTagToProduct(int productId, int tagId)
        {
            await productTagService.AddTagToProductAsync(productId, tagId);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTagRomProductAsync(int productId, int tagId)
        {
            await productTagService.DeleteTagFromProductAsync(productId, tagId);
            return Ok();
        }


    }
}
