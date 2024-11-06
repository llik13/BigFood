using System;
using System.Collections.Generic;
using Catalog.DataAccessLayer.Configuration;
using Catalog.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DataAccessLayer;

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

    public virtual DbSet<Producttag> Producttags { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }
    public virtual DbSet<Ingredient> Ingredients { get; set; }
    
    public virtual DbSet<Review> Reviews { get; set; }
    /*
    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Deliver> Delivers { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    */


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTagConfiguration());
        modelBuilder.ApplyConfiguration(new PromotionConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
    }

}
