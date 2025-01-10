using FribergCarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRental.data
{
    public class ApplicationDbContext : DbContext
    {
        DbSet<Car> Cars { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
