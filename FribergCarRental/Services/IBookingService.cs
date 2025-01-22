using FribergCarRental.Models.Entities;
using FribergCarRental.Models.ViewModel;

namespace FribergCarRental.Services
{
    public interface IBookingService
    {
        bool CarExist(int id);
        Contact GetContactFromUsername(string username);
        User GetUserFromUsernameWithContact(string username);
        Booking CreateBooking(Car car, User user, DateTime startDate, DateTime endDate);
        IEnumerable<BookingIndexViewModel> GetBookingsForUser(int? userId);
        Booking GetById(int bookingId);
        void RemoveBooking(Booking booking);
    }
}
