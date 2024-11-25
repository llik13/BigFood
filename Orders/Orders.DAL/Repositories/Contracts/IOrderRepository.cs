using Orders.DAL.Models;
using Orders.DAL.Pagination;
using Orders.DAL.Pagination.Parameters;

namespace Orders.DAL.Repositories.Contracts;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<PagedList<Order>> GetPaginatedOrders(OrderParameters parameters);
}