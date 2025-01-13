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


        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

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
            //return View(user);
            return View(loginViewModel);
        }

        public IActionResult Logout()
        {
            _authService.Logout();

            return RedirectToAction("Index", "Home");
        }

        [SimpleAuthorize(Role = "Admin")]
        public IActionResult Secret()
        {
            HttpContextAccessor asd = new HttpContextAccessor();
            var user = asd?.HttpContext?.Session.GetString("User");
            ViewBag.User = user;
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
