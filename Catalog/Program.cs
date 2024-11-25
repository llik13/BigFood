
using Catalog.BuisnesDataLayer.DTO.Request;
using Catalog.BuisnesDataLayer.Interfaces;
using Catalog.BuisnesDataLayer.Services;
using Catalog.BuisnesDataLayer.Validation;
using Catalog.DataAccessLayer;
using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Interfaces;
using Catalog.DataAccessLayer.Pagination.Sorting;
using Catalog.DataAccessLayer.Repositories;
using Catalog.DataAccessLayer.Seeding;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Registration.Setting;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Catalog.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<CatalogContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProducttagRepository, ProducttagRepository>();
            builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<ISortHelper<Product>, SortHelper<Product>>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductTagService, ProductTagService>();
            builder.Services.AddScoped<IPromotionService, PromotionService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
           


            // builder.Services.AddValidatorsFromAssemblyContaining<ProductValidation>();
            // builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidation>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
                        ValidateIssuer = true,
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

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            /* 
              using (var scope = app.Services.CreateScope())
              {
                  var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
                  context.Database.Migrate(); 
                  CatalogContextSeeder.Seed(context); 
              }
            */

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
                });
            }



            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
