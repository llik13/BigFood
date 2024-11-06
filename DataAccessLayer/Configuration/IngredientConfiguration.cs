using Catalog.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataAccessLayer.Configuration
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("ingredient");
            builder.HasKey(e => e.IngredientID);
            builder.Property(e => e.Name).HasMaxLength(150);
            builder.Property(e => e.Name).IsRequired(true);

            builder.HasMany(e => e.Products).WithMany(e => e.ProductIngredients)
                .UsingEntity(j => j.ToTable("ProductAndIngridient")); ;
        }
    }
}
