using FribergCarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRental.data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // One-to-one relationship between User and Contact
            modelBuilder.Entity<User>()
                .HasOne(u => u.Contact)
                .WithOne(c => c.User)
                .HasForeignKey<Contact>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <summary>
        /// Used to seed database with initial admin user
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSeeding((context, _) =>
        {
            var user = context.Set<User>().FirstOrDefault(b => b.Username == "admin");

            if (user == null)
            {
                context.Set<User>().Add(new User
                {
                    Username = "admin",
                    Email = "Admin@admin.com",
                    Password = "admin",
                    Role = "Admin",
                    Contact = new Contact
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Address = "Admin",
                        City = "Admin",
                        PostalCode = "127001",
                        Phone = "127001"
                    }
                });
                context.SaveChanges();
            }
        })
        .UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            var user = await context.Set<User>().FirstOrDefaultAsync(b => b.Username == "admin", cancellationToken);
            if (user == null)
            {
                context.Set<User>().Add(new User
                {
                    Username = "admin",
                    Email = "Admin@admin.com",
                    Password = "admin",
                    Role = "Admin",
                    Contact = new Contact
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Address = "Admin",
                        City = "Admin",
                        PostalCode = "127001",
                        Phone = "127001"
                    }
                });
                await context.SaveChangesAsync(cancellationToken);
            }
        });
    }
}
