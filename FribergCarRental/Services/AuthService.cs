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

        /// <summary>
        /// Authenticates a user with the given username and password. 
        /// If successful, sets session values for the user's username and role.
        /// </summary>
        /// <param name="username">
        /// The username or email of the user attempting to log in.
        /// </param>
        /// <param name="password">
        /// The password of the user attempting to log in.
        /// </param>
        /// <returns>
        /// <c>true</c> if the login is successful; otherwise, <c>false</c>.
        /// </returns>
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

        /// <summary>
        /// Registers a new user with the provided registration details. 
        /// Creates a user account, including associated contact information, 
        /// and logs the user in by setting session values for their username and role.
        /// </summary>
        /// <param name="registerViewModel">
        /// The <see cref="RegisterViewModel"/> containing the user's registration details.
        /// </param>
        /// <exception cref="Exception">
        /// Thrown if an error occurs while adding the user to the repository.
        /// </exception>
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

        /// <summary>
        /// Checks whether a user with the specified email address exists in the database.
        /// </summary>
        /// <param name="email">
        /// The email address to check for existence.
        /// </param>
        /// <returns>
        /// <c>true</c> if a user with the given email exists; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> Exists(string email)
        {
            return await _userRepository.AnyAsync(u =>  u.Email == email);
        }

        /// <summary>
        /// Logs out the current user by clearing the session values for username and role.
        /// </summary>
        public void Logout()
        {
            _httpAccessor?.HttpContext?.Session.SetString("User", "");
            _httpAccessor?.HttpContext?.Session.SetString("Role", "");
        }

        /// <summary>
        /// Check if the user is signed in
        /// </summary>
        /// <returns>
        /// <c>true</c> if the user is signed in; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAuthenticated()
        {
            var user = _httpAccessor?.HttpContext?.Session.GetString("User");
            if (string.IsNullOrEmpty(user))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Retrieves the currently authenticated user based on session data.
        /// </summary>
        /// <returns>
        /// A <see cref="User"/> object representing the current user if found; otherwise <c>null</c>
        /// </returns>
        public async Task<User?> GetCurrentUser()
        {
            var username = _httpAccessor?.HttpContext?.Session.GetString("User");
            if (username == null)
            {
                return null;
            }

            return await _userRepository.FindByUsername(username);
        }

        /// <summary>
        /// Retrieves the username of the currently authenticated user from the session data 
        /// and returns it in a capitalized format.
        /// </summary>
        /// <returns>
        /// The capitalized username as a <see cref="string"/>. If no username is found, returns <c>null</c>.
        /// </returns>
        public string GetUsername()
        {
            var username = _httpAccessor?.HttpContext?.Session.GetString("User");

            return username;
        }

        /// <summary>
        /// Checks if user is an admin based on session data.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the user is admin; otherwise <c>false</c>
        /// </returns>
        public async Task<bool> IsAdmin()
        {
            var user = await GetCurrentUser();

            if (user == null || !user.Role.Equals("Admin"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Retrieves the ID of the currently authenticated user.
        /// </summary>
        /// <returns>
        /// The ID of the current user as an <see cref="int"/>.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the current user is not found or the user is not authenticated.
        /// </exception>
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
