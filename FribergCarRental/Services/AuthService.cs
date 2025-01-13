using FribergCarRental.data;
using FribergCarRental.Models;

namespace FribergCarRental.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpAccessor;
        public AuthService(IUserRepository userRepository, IHttpContextAccessor httpContext)
        {
            _userRepository = userRepository;
            _httpAccessor = httpContext;
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.Find( u => (u.Username == username || u.Email == username) && u.Password == password).FirstOrDefault();

            if (user != null)
            {
                _httpAccessor?.HttpContext?.Session.SetString("User", user.Username);
                _httpAccessor?.HttpContext?.Session.SetString("Role", user.Role);
                return true;
            }

            return false;
        }

        public bool Register (string username, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            // TODO: Maybe not clear, but just remove the auth fields
            _httpAccessor?.HttpContext?.Session.Clear();
        }

        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }
    }
}
