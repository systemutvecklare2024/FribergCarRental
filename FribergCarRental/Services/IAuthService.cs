using FribergCarRental.Models;

namespace FribergCarRental.Services
{
    public interface IAuthService
    {
        Task Register(RegisterViewModel registerViewModel);
        bool Login(string username, string password);
        void Logout();
        bool IsAuthenticated();
        User? GetCurrentUser();
        bool IsAdmin();
        public string GetUsername();
        bool Exists(string username, string email);
    }
}
