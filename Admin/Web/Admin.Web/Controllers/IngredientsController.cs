using Aplication.Ingridient.Commands;
using Aplication.Ingridient.Queries;
using Aplication.Ingridient;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.WebUI.Controllers;

namespace Admin.Web.Controllers
{
    [ApiController]
    [Route("api/ingredients")]
    public class IngredientsController : BaseController
    { 
        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientCommand command)
        {
            var ingredientId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetIngredientById), new { id = ingredientId }, null);
        }

        [HttpGet]
        public async Task<ActionResult<List<IngredientDto>>> GetAllIngredients()
        {
            var ingredients = await Mediator.Send(new GetAllIngredientsQuery());
            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDto>> GetIngredientById(int id)
        {
            var ingredient = await Mediator.Send(new GetIngredientByIdQuery(id));
            if (ingredient == null)
            {
                return NotFound();
            }
            return Ok(ingredient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] UpdateIngredientCommand command)
        {
            if (id != command.IngredientID)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            await Mediator.Send(new DeleteIngredientCommand(id));
            return NoContent();
        }

        [HttpPost("{productId}/add/{ingredientId}")]
        public async Task<IActionResult> AddIngredient(int productId, int ingredientId)
        {
            await Mediator.Send(new AddIngredientToProductCommand(productId, ingredientId));
            return NoContent();
        }

        [HttpDelete("{productId}/remove/{ingredientId}")]
        public async Task<IActionResult> RemoveIngredient(int productId, int ingredientId)
        {
            await Mediator.Send(new RemoveIngredientFromProductCommand(productId, ingredientId));
            return NoContent();
        }
    }
}
