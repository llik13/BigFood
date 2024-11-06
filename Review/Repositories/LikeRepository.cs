using MySql.Data.MySqlClient;
using Review.Entitites;
using Review.Interfaces;
using System.Data;

namespace Review.Repositories
{
    public class LikeRepository : GenericRepository<Likes>, ILikeRepository
    {
        public LikeRepository(MySqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName) : base(sqlConnection, dbTransaction, tableName)
        {
        }
    }
}
