using Aplication.Interfaces;
using Aplication.User.Commands;
using Aplication.User;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.WebUI.Controllers;
using Aplication.User.Queries;

namespace Admin.Web.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var userId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, null);
        }

        // Получение всех пользователей
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await Mediator.Send(new GetUsersQuery());
            return Ok(users);
        }

        // Получение пользователя по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await Mediator.Send(new GetUserByIdQuery(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Обновление пользователя
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        // Удаление пользователя
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await Mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}
