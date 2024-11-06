using Domain.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entitites.Order> Order { get; }
        DbSet<Domain.Entitites.User> User { get; }
        DbSet<Domain.Entitites.Product> Product { get; }
        DbSet<Domain.Entitites.Category> Category { get; }
        DbSet<Domain.Entitites.Ingredient> Ingridient { get; }
        DbSet<Domain.Entitites.ProductTag> ProductTags { get; }
        DbSet<Domain.Entitites.Promotion> Promotion { get; }
    
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
