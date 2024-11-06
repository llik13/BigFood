using AutoMapper;
using Dapper;
using Map.Entities;
using Map.Repositories.Interfaces;
using Maps.Repositories.DTO;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace Map.Repositories
{
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
    {
        private readonly IMapper _mapper;
        public DeliveryRepository(MySqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName, IMapper mapper) 
            : base(sqlConnection, dbTransaction, tableName)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<Delivery>> GetByStatusAsync(string status)
        {
            List<Delivery> deliveryList = new List<Delivery>();
            status = status.ToLower();
            string expression = "SELECT * FROM deliveries WHERE status = @Status AND IsRowActive = 1;";
            MySqlCommand command = new MySqlCommand(expression, _sqlConnection);
            MySqlParameter statusParametr = new MySqlParameter("@Status", status);
            command.Parameters.Add(statusParametr);

            using (DbDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Delivery delivery = new Delivery();
                    delivery.id = Convert.ToInt32(reader["id"]);
                    delivery.customerId = Convert.ToInt32(reader["customerId"]);
                    delivery.status = Convert.ToString(reader["status"]);
                    delivery.cost = Convert.ToDecimal(reader["cost"]);
                    delivery.time = Convert.ToDateTime(reader["time"]);
                    delivery.startLatitude = Convert.ToDecimal(reader["startLatitude"]);
                    delivery.endLatitude = Convert.ToDecimal(reader["endLatitude"]);
                    delivery.startLongitude = Convert.ToDecimal(reader["startLongitude"]);
                    delivery.endLongitude = Convert.ToDecimal(reader["endLongitude"]);
                    deliveryList.Add(delivery);
                }
            }
            return deliveryList;
        }

        public async Task<IEnumerable<DeliveryWithDeliverDTO>> GetDeliveriesAndDlivers()
        {
            var sql = @"Select cost, status, time, firstName, lastName, number
                    From deliveries
                    Join delivers on deliverId = delivers.id ";

            var deliveryInformation = await _sqlConnection.QueryAsync<Delivery, Deliver, Delivery>(sql, (delivery, deliver) =>
            {
                delivery.Deliver = deliver;
                return delivery;
            },
            splitOn: "firstName",
            transaction: _dbTransaction
            );
            var deliveryInformationDTO = _mapper.Map<IEnumerable<DeliveryWithDeliverDTO>>(deliveryInformation);

            return deliveryInformationDTO;
        }

        public async Task SetStatusAsync(int id, string status)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@p_DeliveryId", id);
            parameters.Add("@p_NewStatus", status);

            await _sqlConnection.ExecuteAsync(
                sql: "UpdateDeliveryStatus",
                param: parameters,
                transaction: _dbTransaction,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
