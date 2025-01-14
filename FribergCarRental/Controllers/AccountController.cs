using Microsoft.AspNetCore.Mvc;
using FribergCarRental.Models;
using FribergCarRental.Services;
using FribergCarRental.Filters;

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

        // GET: Account/AccessDenied
        [Route("AccessDenied")]
        [Route("Account/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
