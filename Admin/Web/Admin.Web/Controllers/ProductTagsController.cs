using Aplication.ProductTag.Commands;
using Aplication.ProductTag.Queries;
using Aplication.ProductTag;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.WebUI.Controllers;

namespace Admin.Web.Controllers
{
    [ApiController]
    [Route("api/producttags")]
    public class ProductTagsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateProductTag([FromBody] CreateProductTagCommand command)
        {
            var tagId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetProductTagById), new { id = tagId }, null);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductTagDto>>> GetAllProductTags()
        {
            var tags = await Mediator.Send(new GetAllProductTagsQuery());
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTagDto>> GetProductTagById(int id)
        {
            var tag = await Mediator.Send(new GetProductTagByIdQuery (id));
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductTag(int id, [FromBody] UpdateProductTagCommand command)
        {
            if (id != command.TagId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductTag(int id)
        {
            await Mediator.Send(new DeleteProductTagCommand { TagId = id });
            return NoContent();
        }

        [HttpPost("products/{productId}/tags/{tagId}")]
        public async Task<IActionResult> AddTag(int productId, int tagId)
        {
            await Mediator.Send(new AddTagToProductCommand(productId, tagId));
            return NoContent();
        }

        [HttpDelete("products/{productId}/tags/{tagId}")]
        public async Task<IActionResult> RemoveTag(int productId, int tagId)
        {
            await Mediator.Send(new RemoveTagFromProductCommand(productId, tagId));
            return NoContent();
        }
    }
}
