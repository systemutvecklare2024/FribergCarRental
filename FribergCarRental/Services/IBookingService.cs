using FribergCarRental.Models.Entities;

namespace FribergCarRental.Services
{
    public interface IBookingService
    {
        bool CarExist(int id);
        Contact GetContactFromUsername(string username);
        User GetUserFromUsernameWithContact(string username);
    }
}
