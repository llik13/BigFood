using Aplication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Northwind.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Admin.Web.Service;
using Admin.Web.Dto;

namespace Admin.Web.Controllers
{
    
    [ApiController]
    [Route("api/users")]
    public class AdminUserController : ControllerBase
    {
        private readonly UserService _userServiceClient;

        public AdminUserController(UserService userServiceClient)
        {
            _userServiceClient = userServiceClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                var user = await _userServiceClient.GetUserAsync(id);
                return Ok(user);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserAdminDto userDto)
        {
            try
            {
                await _userServiceClient.UpdateUserAsync(id, userDto);
                return NoContent();
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userServiceClient.DeleteUserAsync(id);
                return NoContent();
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }
    }
}
