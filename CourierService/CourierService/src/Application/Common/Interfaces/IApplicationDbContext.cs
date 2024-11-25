using CourierService.Domain.Entities;

namespace CourierService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; set; }

    DbSet<Product> Products { get; set; }

    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
