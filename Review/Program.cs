
using MySql.Data.MySqlClient;
using Review.Interfaces;
using Review.Repositories;
using System.Data;

namespace Review
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

            builder.Services.AddScoped((s) => new MySqlConnection((builder.Configuration.GetConnectionString("MySqlConnection"))));
            builder.Services.AddScoped<IDbTransaction>(s =>
            {
                MySqlConnection conn = s.GetRequiredService<MySqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });


            builder.Services.AddScoped<ICommentRepository>(s =>
            {
                var connection = s.GetRequiredService<MySqlConnection>();
                var transaction = s.GetRequiredService<IDbTransaction>();
                var tableName = "comment";
                return new CommentRepository(connection, transaction, tableName);
            });
            
            builder.Services.AddScoped<IReviewRepository>(s =>
            {
                var connection = s.GetRequiredService<MySqlConnection>();
                var transaction = s.GetRequiredService<IDbTransaction>();
                var tableName = "review";
                return new ReviewRepository(connection, transaction, tableName);
            });

            builder.Services.AddScoped<IUserRepository>(s =>
            {
                var connection = s.GetRequiredService<MySqlConnection>();
                var transaction = s.GetRequiredService<IDbTransaction>();
                var tableName = "user";
                return new UserRepository(connection, transaction, tableName);
            });

            builder.Services.AddScoped<ILikeRepository>(s =>
            {
                var connection = s.GetRequiredService<MySqlConnection>();
                var transaction = s.GetRequiredService<IDbTransaction>();
                var tableName = "likes";
                return new LikeRepository(connection, transaction, tableName);
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
