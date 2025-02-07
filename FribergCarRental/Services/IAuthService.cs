using FribergCarRental.Models.Entities;
using FribergCarRental.Models.ViewModel;

namespace FribergCarRental.Services
{
    public interface IAuthService
    {
        Task Register(RegisterViewModel registerViewModel);
        Task<bool> Login(string username, string password);
        void Logout();
        bool IsAuthenticated();
        Task<User?> GetCurrentUser();
        Task<int> GetCurrentUserId();
        Task<bool> IsAdmin();
        public string GetUsername();
        Task<bool> Exists(string account);
        Task<User?> GetAccount();
    }
}
