using FribergCarRental.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRental.data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        //public DbSet<Receipt> Reciepts { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-one relationship between Booking and Receipt
            //modelBuilder
            //    .Entity<Receipt>()
            //    .HasOne(e => e.Booking)
            //    .WithOne(e => e.Receipt)
            //    .OnDelete(DeleteBehavior.NoAction);


            // One-to-one relationship between User and Contact
            modelBuilder.Entity<User>()
                .HasOne(u => u.Contact)
                .WithOne(c => c.User)
                .HasForeignKey<Contact>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}