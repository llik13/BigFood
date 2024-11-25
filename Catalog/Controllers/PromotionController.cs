using Catalog.BuisnesDataLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;


        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpPost("{productId}/promotions/{promotionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddPromotionToProduct(int productId, int promotionId)
        {
            await _promotionService.AddPtomotionToProductAsync(promotionId, productId);
            return Ok();
        }

        [HttpDelete("{productId}/promotions/{promotionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePromotionFromProduct(int productId, int promotionId)
        {
            await _promotionService.DeletePtomotionRomProductAsync(promotionId, productId);
            return Ok();
        } 
    }
}
