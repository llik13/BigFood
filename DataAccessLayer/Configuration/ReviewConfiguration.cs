using Catalog.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DataAccessLayer.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(e => e.ReviewId).HasName("PRIMARY");

            builder.ToTable("reviews");

            builder.HasIndex(e => e.ProductId, "ProductID");

            builder.Property(e => e.ReviewId).HasColumnName("ReviewID");
            builder.Property(e => e.Comment).HasColumnType("text");
            builder.Property(e => e.ProductId).HasColumnName("ProductID");
            builder.Property(e => e.ReviewDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            builder.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_ibfk_1");


            
            builder.Property(e => e.Comment).IsRequired();
            builder.Property(e => e.Comment).IsRequired(false);

            builder.Property(e => e.Rating).HasColumnType("tinyint unsigned");

        }
    }
}
