using System.Data;
using Forum.DAL.Repositories;
using Forum.DAL.Repositories.Contracts;
using Forum.Helpers;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();


builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

// SWAGER
builder.Services.AddSwaggerGen();

//DATABASE
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped((s) => new NpgsqlConnection(builder.Configuration.GetConnectionString("PostgreSqlConnection")));
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddScoped((s) => new NpgsqlConnection(Environment.GetEnvironmentVariable("DockerConnectionStringPostgres")));
}

builder.Services.AddScoped<IDbTransaction>(s =>
{
    NpgsqlConnection conn = s.GetRequiredService<NpgsqlConnection>();
    conn.Open();
    return conn.BeginTransaction();
});


//DEPENDENCY INJECTION
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// for docker database initialization
//builder.Services.AddScoped<DbContext>();

var app = builder.Build();

// for docker database initialization 
//if(app.Environment.IsProduction())
//{
//    using var scope = app.Services.CreateScope();
//    var context = scope.ServiceProvider.GetRequiredService<DbContext>();
//    await context.InitTables();
//}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
