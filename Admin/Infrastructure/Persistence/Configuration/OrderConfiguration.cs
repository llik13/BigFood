using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Domain.Entitites;

namespace Persistence.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entitites.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entitites.Order> builder)
        {
            builder.ToTable("Orders"); 
            builder.HasKey(o => o.OrderId); 
            builder.Property(o => o.TotalPrice)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId); 
        }
    }
}
