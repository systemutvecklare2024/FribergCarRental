using FribergCarRental.Models.Entities;

namespace FribergCarRental.data
{
    public class BookingRepository : GenericRepository<Booking, ApplicationDbContext>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
