using Microsoft.EntityFrameworkCore;

namespace FribergCarRental.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
