using System.Data;
using Dapper;
using Npgsql;

namespace Forum.Helpers;

public class DbContext
{
    private readonly NpgsqlConnection _sqlConnection;

    private readonly IDbTransaction _dbTransaction;


    public DbContext(NpgsqlConnection sqlConnection, 
        IDbTransaction dbTransaction )
    {
        _sqlConnection = sqlConnection;
        _dbTransaction = dbTransaction;
    }

    public async Task InitTables()
    {
        var sql = "CREATE TABLE users (id SERIAL PRIMARY KEY, username VARCHAR(50) UNIQUE NOT NULL, email VARCHAR(100) UNIQUE NOT NULL, created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP);";
        _sqlConnection.ExecuteAsync(sql);

        sql = "CREATE TABLE categories ( id SERIAL PRIMARY KEY, category_name VARCHAR(100) UNIQUE NOT NULL, description TEXT );";
        _sqlConnection.ExecuteAsync(sql);

        sql = "CREATE TABLE posts ( id SERIAL PRIMARY KEY, title VARCHAR(200) NOT NULL, content TEXT NOT NULL, created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, updated_at TIMESTAMP, user_id INT REFERENCES users(id) ON DELETE CASCADE, category_id INT REFERENCES categories(id) ON DELETE SET NULL );";
        _sqlConnection.ExecuteAsync(sql);

        sql = "CREATE TABLE comments ( id SERIAL PRIMARY KEY, content TEXT NOT NULL, created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, post_id INT REFERENCES posts(id) ON DELETE CASCADE, user_id INT REFERENCES users(id) ON DELETE CASCADE );";
        _sqlConnection.ExecuteAsync(sql);
    }
    
}