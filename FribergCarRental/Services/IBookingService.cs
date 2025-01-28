using FribergCarRental.Areas.Admin.Models;
using FribergCarRental.Models.Entities;
using FribergCarRental.Models.ViewModel;

namespace FribergCarRental.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingIndexViewModel>> GetBookingsForUser(int? userId);
        Task<IEnumerable<Booking>> GetAllBookingsWithDetailsAsync();

        Task<Booking?> GetBookingByIdWithDetailAsync(int id);
        Task<Booking?> CreateBookingAsync(Car car, User user, DateTime dateTime1, DateTime dateTime2);
        Task<Booking?> GetByIdAsync(int id);
        Task UpdateBookingAsync(Booking updatedBooking);
        Task UpdateBookingAsync(AdminBookingViewModel updatedBooking);
        Task RemoveBookingAsync(Booking booking);
    }
}
