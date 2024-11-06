using MySql.Data.MySqlClient;
using Review.Entitites;
using Review.Interfaces;
using System.Data;

namespace Review.Repositories
{
    public class ReviewRepository : GenericRepository<Reviews>, IReviewRepository
    {
        public ReviewRepository(MySqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName) : base(sqlConnection, dbTransaction, tableName)
        {
        }
    }
}
