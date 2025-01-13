namespace FribergCarRental.Services
{
    public interface IAuthService
    {
        bool Register(string username, string password);
        bool Login(string username, string password);
        void Logout();
        bool IsAuthenticated();
    }
}
