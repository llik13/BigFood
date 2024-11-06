using Catalog.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalog.DataAccessLayer.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // reverse engineering 
            
            builder.HasKey(e => e.ProductId).HasName("PRIMARY");

            builder.ToTable("products");

            builder.HasIndex(e => e.CategoryId, "CategoryID");

            builder.Property(e => e.ProductId).HasColumnName("ProductID");
            builder.Property(e => e.Availability).HasDefaultValueSql("'1'");
            builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
            builder.Property(e => e.Description).HasColumnType("text");
            builder.Property(e => e.ImageUrl).HasMaxLength(255);
            builder.Property(e => e.Name).HasMaxLength(255);
            builder.Property(e => e.Price).HasPrecision(10);

            builder.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("products_ibfk_1");


            // after reverse engineering 

            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.CategoryId).IsRequired();
            
            builder.Property(e => e.Rating).HasColumnType("decimal(1,1)");
            builder.ToTable(t => t.HasCheckConstraint("ValidRating", "Rating >= 0 AND Rating <= 5"));
            builder.Property(e => e.Rating)
            .HasColumnType("decimal(1,1)")
            .IsRequired(false);
            builder.Property(e => e.Description).HasMaxLength(400);
        }
    }
}
