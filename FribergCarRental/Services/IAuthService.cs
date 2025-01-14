using FribergCarRental.Models;

namespace FribergCarRental.Services
{
    public interface IAuthService
    {
        bool Register(string username, string email, string password);
        bool Login(string username, string password);
        void Logout();
        bool IsAuthenticated();
        User? GetCurrentUser();
        bool IsAdmin();
    }
}
