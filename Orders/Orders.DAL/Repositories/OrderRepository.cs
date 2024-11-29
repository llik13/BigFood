using Microsoft.EntityFrameworkCore;
using Orders.DAL.Models;
using Orders.DAL.Pagination;
using Orders.DAL.Pagination.Parameters;
using Orders.DAL.Repositories.Contracts;

namespace Orders.DAL.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(OrdersContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Order>> GetAllAsync()
    {
        var result = await table
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.Product)
            .ToListAsync();
        return result;
    }

    public override async Task<Order> GetByIdAsync(int id)
    {
        //var result = await table.Include(o => o.Product).FirstOrDefaultAsync();
        var result = await table
            .Where(o => o.Id == id)
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.Product)
            .SingleOrDefaultAsync();
        return result;
    }



    public async Task<PagedList<Order>> GetPaginatedOrders(OrderParameters parameters)
    {
        return await PagedList<Order>.ToPagedListAsync(table.AsQueryable(), parameters.PageNumber, parameters.PageSize);
    }
}