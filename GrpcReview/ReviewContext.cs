using Microsoft.EntityFrameworkCore;

namespace GrpcReview
{
    public class ReviewContext : DbContext
    {
        public ReviewContext()
        {
        }

        public ReviewContext(DbContextOptions<ReviewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Review> Reviews { get; set; }
     

        /*
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Deliver> Delivers { get; set; }

        public virtual DbSet<Delivery> Deliveries { get; set; }

        */


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
