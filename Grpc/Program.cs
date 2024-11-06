using Grpc.Services;
using Microsoft.EntityFrameworkCore;

namespace Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();

            builder.Services.AddDbContext<CatalogContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            var app = builder.Build();



            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<AdminService>();
            app.MapGrpcService<ProductService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}