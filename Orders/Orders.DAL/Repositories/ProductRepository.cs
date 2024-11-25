using Orders.DAL.Models;
using Orders.DAL.Repositories.Contracts;

namespace Orders.DAL.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(OrdersContext context) : base(context)
    {
    }
}