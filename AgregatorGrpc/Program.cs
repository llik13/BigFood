using AgregatorGrpc.Protos;
using AgregatorGrpc.Services;

namespace AgregatorGrpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddGrpcClient<Products.ProductsClient>(o =>
            {
                o.Address = new Uri("https://localhost:7007");
            });

            builder.Services.AddGrpcClient<Reviews.ReviewsClient>(o =>
            {
                o.Address = new Uri("https://localhost:7008");
            });

            var app = builder.Build();



            

            app.MapGrpcService<AggregatorService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}