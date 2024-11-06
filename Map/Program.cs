
using Map.Repositories.Interfaces;
using Map.Repositories;
using System.Data;
using MySql.Data.MySqlClient;

namespace Map
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

            //DATABASE
            builder.Services.AddScoped((s) => new MySqlConnection((builder.Configuration.GetConnectionString("MySqlConnection"))));
            builder.Services.AddScoped<IDbTransaction>(s =>
            {
                MySqlConnection conn = s.GetRequiredService<MySqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });

            //DEPENDENCY INJECTION
            builder.Services.AddScoped<IDeliverRepository>(s =>
            {
                var connection = s.GetRequiredService<MySqlConnection>();
                var transaction = s.GetRequiredService<IDbTransaction>();
                var tableName = "delivers";
                return new DeliverRepository(connection, transaction, tableName);
            });
            builder.Services.AddScoped<IDeliveryRepository>(s =>
            {
                var connection = s.GetRequiredService<MySqlConnection>();
                var transaction = s.GetRequiredService<IDbTransaction>();
                var tableName = "deliveries";
                return new DeliveryRepository(connection, transaction, tableName);
            });
            builder.Services.AddScoped<ICustomerRepository>(s =>
            {
                var connection = s.GetRequiredService<MySqlConnection>();
                var transaction = s.GetRequiredService<IDbTransaction>();
                string tableName = "customers";
                return new ÑustomerRepository(connection, transaction, tableName);
            });

            builder.Services.AddScoped<IUnitOfWork, UnitofWork>();

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
