using System.ComponentModel.DataAnnotations;
using System.Data;
using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Npgsql;

namespace Forum.DAL.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(NpgsqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "categories")
    {
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = new List<Category>();
        using var cmd = _sqlConnection.CreateCommand();    
        cmd.CommandText = "SELECT * FROM categories";
        using var reader = await cmd.ExecuteReaderAsync();
        if (reader is not null)
        {
            while (await reader.ReadAsync())
            {
                categories.Add(new Category
                {
                    id = reader.GetInt32(reader.GetOrdinal("id")),
                    category_name = reader.GetString(reader.GetOrdinal("category_name")),
                    description = reader.GetString(reader.GetOrdinal("description")),
                });
            }
        }

        return categories;
    }

    public override async Task<Category> GetAsync(int id)
    {
        Category category = new Category();
        using var cmd = _sqlConnection.CreateCommand();    
        cmd.CommandText = "SELECT * FROM categories Where id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = await cmd.ExecuteReaderAsync();
        if (reader is not null)
        {
            while (await reader.ReadAsync())
            {
                category = new Category
                {
                    id = reader.GetInt32(reader.GetOrdinal("id")),
                    category_name = reader.GetString(reader.GetOrdinal("category_name")),
                    description = reader.GetString(reader.GetOrdinal("description")),
                };
            }
        }
        return category;
    }
    
    public override async Task DeleteAsync(int id)
    {
        const string deleteQuery = "DELETE FROM categories WHERE id = @id";
        using var cmd = _sqlConnection.CreateCommand();
        cmd.CommandText = deleteQuery;
        cmd.Parameters.AddWithValue("@id", id);
        var rowAffected = await cmd.ExecuteNonQueryAsync();
    }
    
    public override async Task<int> AddAsync(Category category)
    {
        const string insertQuery =
            "INSERT INTO categories (category_name, description) " +
            "VALUES (@category_name, @description);";

        using var cmd = _sqlConnection.CreateCommand();
        cmd.CommandText = insertQuery;
        cmd.Parameters.AddWithValue("@category_name", category.category_name);
        cmd.Parameters.AddWithValue("@description", category.description);
        var rowAffected = await cmd.ExecuteNonQueryAsync();
        return rowAffected;
    }
    
    public override async Task ReplaceAsync(Category category, int? id = null)
    {
        const string updateQuery =
            "UPDATE categories SET category_name = @category_name, description = @description WHERE id = @id";

        using var cmd = _sqlConnection.CreateCommand();
        cmd.CommandText = updateQuery;
        cmd.Parameters.AddWithValue("@category_name", category.category_name);
        cmd.Parameters.AddWithValue("@description", category.description);
        cmd.Parameters.AddWithValue("@id", category.id);
        var rowAffected = await cmd.ExecuteNonQueryAsync();
    }
}