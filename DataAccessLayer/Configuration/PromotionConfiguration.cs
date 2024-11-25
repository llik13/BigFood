using Catalog.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DataAccessLayer.Configuration
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.HasKey(e => e.PromotionId).HasName("PRIMARY");

            builder.ToTable("promotions");

            builder.HasIndex(e => e.ProductId, "ProductID");

            builder.Property(e => e.PromotionId).HasColumnName("PromotionID");
            builder.Property(e => e.DiscountPercentage).HasPrecision(5);
            builder.Property(e => e.EndDate).HasColumnType("datetime");
            builder.Property(e => e.ProductId).HasColumnName("ProductID");
            builder.Property(e => e.StartDate).HasColumnType("datetime");

            builder.HasMany(d => d.Products).WithMany(p => p.Promotions)
                .UsingEntity(j => j.ToTable("PromotionAndProducts"));

            builder.ToTable(t => t.HasCheckConstraint("ValidPercentage", "DiscountPercentage >= 0 AND DiscountPercentage <= 50"));
        }
    }
 }
