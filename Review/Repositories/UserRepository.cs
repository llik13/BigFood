using MySql.Data.MySqlClient;
using Review.Entitites;
using Review.Interfaces;
using System.Data;

namespace Review.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MySqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName) : base(sqlConnection, dbTransaction, tableName)
        {
        }
    }
}
