using System.Data;
using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Npgsql;

namespace Forum.DAL.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository 
{
    public CommentRepository(NpgsqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "comments")
    {
    }

    public async Task<IEnumerable<Comment>> GetByPostId(int post_id)
    {
        var result = await _sqlConnection.QueryAsync<Comment>($"SELECT * FROM {_tableName} WHERE post_id=@post_id", 
            param: new { post_id = post_id }, 
            transaction: _dbTransaction);
        if (result == null)
            throw new KeyNotFoundException($"comments with post_id [{post_id}] could not be found.");
        return result;
    }

    public async Task<IEnumerable<Comment>> GetByUserId(int user_id)
    {
        var result = await _sqlConnection.QueryAsync<Comment>($"Select * FROM {_tableName} Where user_id=@user_id",
            param: new  { user_id = user_id },
            transaction: _dbTransaction);
        if (result == null)
            throw new KeyNotFoundException($"comments with user_id [{user_id}] could not be found.");
        return result;
    }
}