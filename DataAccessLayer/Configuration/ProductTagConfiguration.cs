using Catalog.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataAccessLayer.Configuration
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<Producttag>
    {
        public void Configure(EntityTypeBuilder<Producttag> builder)
        {
            builder.HasKey(e => e.TagId);
            builder.Property(e => e.TagName).HasMaxLength(255);
            builder.Property(e => e.TagName).HasColumnName("name");
            builder.HasMany(e => e.Product).WithMany(e => e.Producttags)
                .UsingEntity(j => j.ToTable("ProductTagWithProduct"));
        }
    }
}