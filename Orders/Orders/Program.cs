using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Orders.AutomapperProfile;
using Orders.BLL.DTO.Requests;
using Orders.BLL.Grpc.Services;
using Orders.BLL.Services;
using Orders.BLL.Services.Contrancts;
using Orders.BLL.Validation;
using Orders.DAL.Models;
using Orders.DAL.Repositories;
using Orders.DAL.Repositories.Contracts;
using Orders.DAL.UOF;
using Orders.Infrastructure;
using Orders.BLL.Services.Contrancts;

namespace Orders;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();

        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);
        
        
        // Adding Authentication - JWT
        //builder.Services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(o =>
        //    {
        //        o.RequireHttpsMetadata = false;
        //        o.SaveToken = false;
        //        o.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            ValidateIssuer = true,
        //            ValidateAudience = false,
        //            ValidateLifetime = true,
        //            ClockSkew = TimeSpan.Zero,

        //            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        //            ValidAudience = builder.Configuration["JWT:ValidAudience"],
        //            IssuerSigningKey =
        //                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        //        };

        //        o.Events = new JwtBearerEvents
        //        {
        //            OnTokenValidated = context =>
        //            {
        //                var token = context.SecurityToken as JwtSecurityToken;
        //                if (token != null)
        //                {
        //                    var audienceClaim = token.Claims.FirstOrDefault(c => c.Type == "audience")?.Value;
        //                    if (audienceClaim != null)
        //                    {
        //                        var audienceFromToken = audienceClaim.Split(',').Select(a => a.Trim()).ToList();
        //                        if (!audienceFromToken.Contains(builder.Configuration["JWT:Audience"]))
        //                        {
        //                            context.Fail("Invalid token");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("Valid audience found");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        context.Fail("no audience claim");
        //                    }
        //                }

        //                return Task.CompletedTask;
        //            }

        //        };
        //    });
        //
        //// SWAGER
        builder.Services.AddSwaggerGen();
        //builder.Services.AddSwaggerGen(options =>
        //{
        //    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        //    {
        //        Name = "Authorization",
        //        In = ParameterLocation.Header,
        //        Type = SecuritySchemeType.Http,
        //        Scheme = "Bearer"
        //    });

        //    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //        {
        //            new OpenApiSecurityScheme
        //            {
        //                Reference = new OpenApiReference
        //                {
        //                    Type = ReferenceType.SecurityScheme,
        //                    Id = "Bearer"
        //                }
        //            },
        //            Array.Empty<string>()
        //        }
        //    });
        //});

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<OrdersContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreeeSQLConnection")));
        }
        else if (builder.Environment.IsProduction())
        {
            builder.Services.AddDbContext<OrdersContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionStringPostgres")));
        }

        // REPOSITORIES
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // SERVICES
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IUserService, UserService>();
        
        // VALIDATORS
        builder.Services.AddScoped<IValidator<OrderRequest>, OrderRequestValidator>();
        
        // gRPC
        builder.Services.AddGrpc(opt => 
        {
            opt.EnableDetailedErrors = true;
        });
        
        // AUTOMAPPER
        //builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
            app.UseSwagger();
            app.UseSwaggerUI();
           // app.UseSwaggerUI(c => // UseSwaggerUI Protected by if (env.IsDevelopment())
           // {
           //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
           //     c.RoutePrefix = string.Empty;
           // });
        //}

        app.UseHttpsRedirection();
        app.UseExceptionHandler();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        //app.UseAuthorization();

        //var summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //app.MapGet("/weatherforecast", (HttpContext httpContext) =>
        //    {
        //        var forecast = Enumerable.Range(1, 5).Select(index =>
        //                new WeatherForecast
        //                {
        //                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //                    TemperatureC = Random.Shared.Next(-20, 55),
        //                    Summary = summaries[Random.Shared.Next(summaries.Length)]
        //                })
        //            .ToArray();
        //        return forecast;
        //    })
        //    .WithName("GetWeatherForecast")
        //    .WithOpenApi();

        app.MapGrpcService<GrpcOrderService>();

        app.Run();
    }
}