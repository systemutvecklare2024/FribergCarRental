using FribergCarRental.data;
using FribergCarRental.Models.Entities;
using FribergCarRental.Models.ViewModel;

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

        public async Task<bool> Login(string username, string password)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => (u.Username == username || u.Email == username) && u.Password == password);

            if (user != null)
            {
                _httpAccessor?.HttpContext?.Session.SetString("User", user.Username);
                _httpAccessor?.HttpContext?.Session.SetString("Role", user.Role);
                return true;
            }

            return false;
        }

        public async Task Register(RegisterViewModel registerViewModel)
        {
            var user = new User
            {
                Username = registerViewModel.Username,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
                Contact = new Contact
                {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    Address = registerViewModel.Address,
                    City = registerViewModel.City,
                    PostalCode = registerViewModel.PostalCode,
                    Phone = registerViewModel.Phone
                }
            };

            try
            {
                var newUser = await _userRepository.AddAsync(user);
            }
            catch (Exception)
            {
                throw;
            }

            // Login
            _httpAccessor?.HttpContext?.Session.SetString("User", user.Username);
            _httpAccessor?.HttpContext?.Session.SetString("Role", user.Role);
        }

        public async Task<bool> Exists(string email)
        {
            return await _userRepository.AnyAsync(u =>  u.Email == email);
        }

        public void Logout()
        {
            _httpAccessor?.HttpContext?.Session.SetString("User", "");
            _httpAccessor?.HttpContext?.Session.SetString("Role", "");
        }

        public bool IsAuthenticated()
        {
            var user = _httpAccessor?.HttpContext?.Session.GetString("User");
            if (string.IsNullOrEmpty(user))
            {
                return false;
            }

            return true;
        }

        public async Task<User?> GetCurrentUser()
        {
            var username = _httpAccessor?.HttpContext?.Session.GetString("User");
            if (username == null)
            {
                return null;
            }

            return await _userRepository.FindByUsername(username);
        }

        public string GetUsername()
        {
            var username = _httpAccessor?.HttpContext?.Session.GetString("User");

            return Utils.StringHelper.Capitalize(username);
        }

        public async Task<bool> IsAdmin()
        {
            var user = await GetCurrentUser();

            if (user == null || !user.Role.Equals("Admin"))
            {
                return false;
            }

            return true;
        }

        public async Task<int> GetCurrentUserId()
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                throw new KeyNotFoundException("Oväntat fel, logga in igen.");
            }
            return user.Id;
        }
    }
}
