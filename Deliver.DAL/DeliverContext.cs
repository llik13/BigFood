using Deliver.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.DAL
{
    public class DeliverContext : DbContext
    {    
        public DbSet<DeliverStats> CourierStats { get; set; }
        public DbSet<DeliverSchedule> CourierSchedules { get; set; }
        public DbSet<DeliverModel> CourierModel { get; set; }

        public DeliverContext(DbContextOptions options) : base(options)
        {
        }

        protected DeliverContext()
        {
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeliverStats>(entity =>
        {
            entity.HasKey(e => e.CourierStatsId);
            entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");
        });
        modelBuilder.Entity<DeliverModel>(entity =>
        {
            entity.HasKey(e => e.CourierId);
        });


            modelBuilder.Entity<DeliverSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId);

            entity.Property(e => e.DayOfWeek)
                .HasConversion(
                    v => v.ToString(),
                    v => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), v));
        });

    }
}
}
