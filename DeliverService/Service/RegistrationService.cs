using JWTAuthentication.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeliverService.Service
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegistrationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        
        public async Task<ApplicationUser> GetDeliverById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7006/api/User/tokens/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            
            var user = JsonConvert.DeserializeObject<ApplicationUser>(content);

            return user;
        }
        
    }
}
