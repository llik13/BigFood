using Dapper;
using MySql.Data.MySqlClient;
using Review.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Review.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected MySqlConnection _sqlConnection;

        protected IDbTransaction _dbTransaction;

        private readonly string _tableName;

        protected GenericRepository(MySqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName)
        {
            _sqlConnection = sqlConnection;
            _dbTransaction = dbTransaction;
            _tableName = tableName;
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _sqlConnection.QueryAsync<T>($"SELECT * FROM {_tableName}",
                transaction: _dbTransaction);
        }

        public async Task<T> GetAsync(int id)
        {
            var result = await _sqlConnection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id=@Id",
                param: new { Id = id },
                transaction: _dbTransaction);
            if (result == null)
                throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _sqlConnection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id=@Id",
                param: new { Id = id },
                transaction: _dbTransaction);
        }


        public async Task<int> AddAsync(T t)
        {
            var insertQuery = GenerateInsertQuery();
            var parameters = new DynamicParameters();

            foreach (var prop in GetProperties)
            {
                var value = prop.GetValue(t);
                if (value is DateTime dateTime)
                {

                    parameters.Add(prop.Name, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    parameters.Add(prop.Name, value);
                }
            }
            var newId = await _sqlConnection.ExecuteScalarAsync<int>(insertQuery,
                param: parameters,
                transaction: _dbTransaction);
            return newId;
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> list)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();
            inserted += await _sqlConnection.ExecuteAsync(query,
                param: list);
            return inserted;
        }


        public async Task ReplaceAsync(T t)
        {
            var updateQuery = GenerateUpdateQuery();
            await _sqlConnection.ExecuteAsync(updateQuery,
                param: t,
                transaction: _dbTransaction);
        }

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        private string GenerateUpdateQuery()
        {
            var properties = GenerateListOfProperties(GetProperties);
            var setClause = string.Join(", ", properties.Where(prop => !prop.Equals("id", StringComparison.OrdinalIgnoreCase))
                .Select(prop => $"{prop}=@{prop}"));

            return $"UPDATE {_tableName} SET {setClause} WHERE Id=@Id;";
        }

        private string GenerateInsertQuery()
        {
            var properties = GenerateListOfProperties(GetProperties);
            properties.Remove("id");

            var columns = string.Join(", ", properties);
            var values = string.Join(", ", properties.Select(prop => $"@{prop}"));

            return $"INSERT INTO {_tableName} ({columns}) VALUES ({values});  SELECT LAST_INSERT_ID();";
        }

    }
}
