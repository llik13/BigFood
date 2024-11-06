using Map.Repositories.Interfaces;
using System.Data;

namespace Map.Repositories
{
    public class UnitofWork : IUnitOfWork, IDisposable
    {

        public ICustomerRepository _customerRepository {  get; }
        public IDeliveryRepository _deliveryRepository { get; }
        public IDeliverRepository _deliverRepository { get; }

        IDbTransaction _dbTransaction;

        public UnitofWork(ICustomerRepository customerRepository, IDbTransaction dbTransaction, IDeliveryRepository deliveryRepository, IDeliverRepository deliverRepository)
        {
            _customerRepository = customerRepository;
            _dbTransaction = dbTransaction;
            _deliveryRepository = deliveryRepository;
            _deliverRepository = deliverRepository;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                // By adding this we can have muliple transactions as part of a single request
                //_dbTransaction.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}
