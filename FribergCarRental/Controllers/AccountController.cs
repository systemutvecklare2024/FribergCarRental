using Microsoft.AspNetCore.Mvc;
using FribergCarRental.Services;
using FribergCarRental.Filters;
using FribergCarRental.Models.ViewModel;

namespace FribergCarRental.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }


        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_authService.Login(loginViewModel.Account, loginViewModel.Password))
                {
                    return RedirectToAction("Secret");
                } else
                {
                    ModelState.AddModelError("", "Invalid account or password.");
                }
            }
            return View(loginViewModel);
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            _authService.Logout();

            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Secret
        [SimpleAuthorize(Role = "Admin")]
        public IActionResult Secret()
        {
            HttpContextAccessor asd = new HttpContextAccessor();
            var user = asd?.HttpContext?.Session.GetString("User");
            ViewBag.User = user;
            return View();
        }

        // GET: Account/Profile
        public IActionResult Profile()
        {

            return View();
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {

            if(_authService.Exists(registerViewModel.Username, registerViewModel.Email))
            {
                ModelState.AddModelError("", "An account with this email or username already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            try
            {
                _authService.Register(registerViewModel);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occured while registring, please try again.");

                return View(registerViewModel);
            }
            return RedirectToAction("Profile", "Account");
        }

        // GET: Account/AccessDenied
        [Route("AccessDenied")]
        [Route("Account/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
