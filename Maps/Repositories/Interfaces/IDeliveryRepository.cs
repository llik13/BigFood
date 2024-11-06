using Map.Entities;
using Maps.Repositories.DTO;

namespace Map.Repositories.Interfaces
{
    public interface IDeliveryRepository : IGenericRepository<Delivery>
    {      
        Task<IEnumerable<Delivery>> GetByStatusAsync(string status);
        Task<IEnumerable<DeliveryWithDeliverDTO>> GetDeliveriesAndDlivers();
        Task SetStatusAsync(int id, string status);
    }
}
