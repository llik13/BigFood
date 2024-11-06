using Domain.Entitites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
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
