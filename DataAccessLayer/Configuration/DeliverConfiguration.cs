using Catalog.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DataAccessLayer.Configuration
{
    public class DeliverConfiguration : IEntityTypeConfiguration<Deliver>
    {
        public void Configure(EntityTypeBuilder<Deliver> builder)
        {
            builder.ToTable("delivers");
        }
    }
}
