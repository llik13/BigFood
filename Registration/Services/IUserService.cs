﻿using JWTAuthentication.WebApi.Models;

namespace JWTAuthentication.WebApi.Services
{
    public interface IUserService
    {


        Task<string> RegisterAsync(RegisterModel model);






        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);







        Task<string> AddRoleAsync(AddRoleModel model);

        Task<AuthenticationModel> RefreshTokenAsync(string jwtToken);

        bool RevokeToken(string token);
        ApplicationUser GetById(string id);
    }
}