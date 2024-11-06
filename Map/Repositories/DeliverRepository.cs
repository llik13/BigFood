using Map.Entities;
using Map.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace Map.Repositories
{
    public class DeliverRepository : GenericRepository<Deliver>, IDeliverRepository
    {
        public DeliverRepository(MySqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName) : base(sqlConnection, dbTransaction, tableName)
        {
        }
    }
}
