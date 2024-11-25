using Orders.DAL.Models;
using Orders.DAL.Repositories.Contracts;

namespace Orders.DAL.UOF;

public class UnitOfWork : IUnitOfWork
{
    private readonly OrdersContext _context;
    public IOrderRepository OrderRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWork(
        OrdersContext context,
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _context = context;
        OrderRepository = orderRepository;
        ProductRepository = productRepository;
        UserRepository = userRepository;
    }
    
    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}