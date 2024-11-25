using Aplication.Promotion;
using Aplication.Promotion.Commands;
using Aplication.Promotion.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.WebUI.Controllers;
using static System.Net.Mime.MediaTypeNames;

namespace Admin.Web.Controllers
{
    [ApiController]
    [Route("api/promotions")]
    public class PromotionsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionCommand command)
        {
            var promotionId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetPromotionById), new { id = promotionId }, null);
        }

        [HttpGet]
        public async Task<ActionResult<List<PromotionDto>>> GetAllPromotions()
        {
            var promotions = await Mediator.Send(new GetAllPromotionsQuery());
            return Ok(promotions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionDto>> GetPromotionById(int id)
        {
            var promotion = await Mediator.Send(new GetPromotionByIdQuery(id));
            if (promotion == null)
            {
                return NotFound();
            }
            return Ok(promotion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromotion(int id, [FromBody] UpdatePromotionCommand command)
        {
            if (id != command.PromotionId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            await Mediator.Send(new DeletePromotionCommand { PromotionId = id });
            return NoContent();
        }

        [HttpPost("{promotionId}/products/{productId}")]
        public async Task<IActionResult> AddProductToPromotion(int promotionId, [FromBody] PromotionDto promotion)
        {
            await Mediator.Send(new AddPromotionToProductCommand(promotionId, promotion));
            return NoContent();
        }

        [HttpDelete("{promotionId}/products/{productId}")]
        public async Task<IActionResult> RemoveProductFromPromotion(int promotionId, int productId)
        {
            await Mediator.Send(new RemovePromotionFromProductCommand(promotionId, productId));
            return NoContent();
        }
    }

}
