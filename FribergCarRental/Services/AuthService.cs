using FribergCarRental.data;
using FribergCarRental.data.UnitOfWork;
using FribergCarRental.Models.Entities;
using FribergCarRental.Models.ViewModel;

namespace FribergCarRental.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IUserContactUnitOfWork _UserContactUnitOfWork;
        public AuthService(IUserRepository userRepository, IHttpContextAccessor httpContext, IUserContactUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _httpAccessor = httpContext;
            _UserContactUnitOfWork = unitOfWork;
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.Find(u => (u.Username == username || u.Email == username) && u.Password == password).FirstOrDefault();

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
                _UserContactUnitOfWork.BeginTransaction();

                _UserContactUnitOfWork.UserRepository.Add(user);
                await _UserContactUnitOfWork.SaveChangesAsync();

                _UserContactUnitOfWork.Commit();
            }
            catch (Exception)
            {
                _UserContactUnitOfWork.Rollback();
                throw;
            }

            // Login
            _httpAccessor?.HttpContext?.Session.SetString("User", user.Username);
            _httpAccessor?.HttpContext?.Session.SetString("Role", user.Role);
        }

        public bool Exists(string email)
        {
            return _userRepository.Any(u =>  u.Email == email);
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

        public User? GetCurrentUser()
        {
            var username = _httpAccessor?.HttpContext?.Session.GetString("User");
            if (username == null)
            {
                return null;
            }

            return _userRepository.FindByUsername(username);
        }

        public string GetUsername()
        {
            var username = _httpAccessor?.HttpContext?.Session.GetString("User");

            return Utils.StringHelper.Capitalize(username);
        }

        public bool IsAdmin()
        {
            var user = GetCurrentUser();

            if (user == null || !user.Role.Equals("Admin"))
            {
                return false;
            }

            return true;
        }
    }
}
