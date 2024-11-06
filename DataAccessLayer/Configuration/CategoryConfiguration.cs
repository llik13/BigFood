using Catalog.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DataAccessLayer.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // reverse engineering 
            builder.HasKey(e => e.CategoryId).HasName("PRIMARY");

            builder.ToTable("categories");

            builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
            builder.Property(e => e.Description).HasColumnType("text");
            builder.Property(e => e.Name).HasMaxLength(255);


            // after reverse engineering
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.Description).IsRequired();

        }
    }
}
