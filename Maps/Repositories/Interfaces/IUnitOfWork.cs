namespace Map.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository _customerRepository { get; }
        IDeliveryRepository _deliveryRepository { get; }
        IDeliverRepository _deliverRepository { get; }
        void Commit();
        void Dispose();
    }
}
