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
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("ingridient");
            builder.HasKey(e => e.IngredientID);
            builder.Property(e => e.Name).HasMaxLength(150);
            builder.Property(e => e.Name).IsRequired(true);

            builder.HasMany(e => e.ProductIngredients).WithMany(e => e.ProductIngredients)
                .UsingEntity(j => j.ToTable("PromotionAndIngridient")); ;
        }
    }
}
