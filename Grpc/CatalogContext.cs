
using Microsoft.EntityFrameworkCore;


namespace Grpc;

public partial class CatalogContext : DbContext
{
    public CatalogContext()
    {
    }

    public CatalogContext(DbContextOptions<CatalogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    /*
    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Deliver> Delivers { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    */


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
