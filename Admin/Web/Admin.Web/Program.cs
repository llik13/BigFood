using Application;
using Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Admin.Web.Service;
using Microsoft.AspNetCore.Identity;
using JWTAuthentication.WebApi.Models;
using JWTAuthentication.WebApi.Contexts;


namespace Admin.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.AddHttpClient<UserService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiSettings:UserUrl"]); // URL микросервиса пользователей
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API", Version = "v1" });


                // Добавляем конфигурацию для JWT токена
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Введите JWT с префиксом Bearer в поле",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
                });
            });

            var jwt = builder.Configuration.GetSection("JWT").Get<jwtConfig>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt.ValidIssuer,
                        ValidAudience = jwt.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Secret))
                    };
                    option.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = contex =>
                        {
                            var token = contex.SecurityToken as JwtSecurityToken;
                            if (token != null)
                            {
                                var audienceClaim = token.Claims.FirstOrDefault(c => c.Type == "aud")?.Value;
                                if (audienceClaim != null)
                                {
                                    var audiencesFromToken = audienceClaim.Split(',').Select(a => a.Trim()).ToList();
                                    var validAudience = jwt?.ValidAudience;
                                    if (!audiencesFromToken.Contains(validAudience))
                                    {
                                        contex.Fail($"Invalid audience error! Expected '{validAudience}', but token contains {string.Join(", ", audienceClaim)}");
                                    }
                                    else
                                    {
                                        contex.Fail("No audience found in the token.");
                                    }
                                }
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
