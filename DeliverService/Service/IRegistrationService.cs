using JWTAuthentication.WebApi.Models;

namespace DeliverService.Service
{
    public interface IRegistrationService
    {
        Task<ApplicationUser> GetDeliverById(string id);
    }
}
