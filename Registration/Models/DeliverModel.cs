using JWTAuthentication.WebApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace Registration.Models
{
    public class DeliverModel : IdentityUser
    {
       

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
