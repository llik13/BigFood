using Orders.DAL.Repositories.Contracts;

namespace Orders.DAL.UOF;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }
    IProductRepository ProductRepository { get; }
    IUserRepository UserRepository { get; }

    Task<int> CompleteAsync(CancellationToken cancellationToken = default(CancellationToken));
}