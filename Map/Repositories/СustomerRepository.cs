using Map.Entities;
using Map.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace Map.Repositories
{
    public class СustomerRepository : GenericRepository<Customers>, ICustomerRepository
    {
        public СustomerRepository(MySqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName) : base(sqlConnection, dbTransaction, tableName)
        {
        }
    }
}
