using System.Data;
using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Npgsql;

namespace Forum.DAL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(NpgsqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "users")
    {
    }

    public override async Task<User> GetAsync(int id)
    {
        var query = "SELECT * FROM get_user_by_id(1);";
        var user = await _sqlConnection.QueryAsync<User>(
            sql: query,
            param: new { user_id = id },
            commandType: CommandType.Text
        );
        
        var result = user.FirstOrDefault();
        return result;
    }

}