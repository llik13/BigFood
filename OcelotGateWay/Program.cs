
using Microsoft.Extensions.Configuration;
using MMLib.Ocelot.Provider.AppConfiguration;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotGateWay
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddOcelotWithSwaggerSupport(options =>
            {
                options.Folder = "OcelotConfiguration";
            });
            builder.Services.AddOcelot(builder.Configuration).AddAppConfiguration();
            builder.Services.AddSwaggerForOcelot(builder.Configuration);

            // Добавление контроллеров
            builder.Services.AddControllers();

            // Swagger в проекте
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";

            });
            app.UseWebSockets();
            app.UseOcelot().Wait();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }
    }
}
