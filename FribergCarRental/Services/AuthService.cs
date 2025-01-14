﻿using FribergCarRental.data;
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

        public bool Register(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            _httpAccessor?.HttpContext?.Session.SetString("User", "");
            _httpAccessor?.HttpContext?.Session.SetString("Role", "");
        }

        public bool IsAuthenticated()
        {
            var user = _httpAccessor?.HttpContext?.Session.GetString("User");
            if(string.IsNullOrEmpty(user))
            {
                return false;
            }

            return true;
        }

        public User? GetCurrentUser()
        {
            var username = _httpAccessor?.HttpContext?.Session.GetString("User");
            if (username == null){
                return null;
            }

            return _userRepository.FindByUsername(username);
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
