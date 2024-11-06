using Registration.Models;

namespace Registration.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string token);
        Task<string> AddRoleAsync(AddRoleModel model);
        ApplicationUser GetById(string id);
        bool RevokeToken(string token);
    }
}
