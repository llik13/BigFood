using Admin.Web.Dto;
using JWTAuthentication.WebApi.Models;

namespace Admin.Web.Service
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            var response = await _httpClient.GetAsync($"api/UserAdmin/{id}");
            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadFromJsonAsync<ApplicationUser>();
            return user;

        }

        public async Task UpdateUserAsync(string id, UpdateUserAdminDto userDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/UserAdmin/{id}", userDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUserAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"/api/UserAdmin/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
