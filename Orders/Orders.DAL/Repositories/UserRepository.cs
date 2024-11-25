using Orders.DAL.Models;

namespace Orders.DAL.Repositories.Contracts;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(OrdersContext context) : base(context)
    {
    }
}