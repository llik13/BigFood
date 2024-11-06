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
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.HasKey(e => e.TagId);
            builder.Property(e => e.TagName).HasMaxLength(255);
            builder.Property(e => e.TagName).HasColumnName("name");
            builder.HasMany(e => e.Product).WithMany(e => e.ProductTags)
                .UsingEntity(j => j.ToTable("ProductTagWithProduct"));
        }
    }
}
