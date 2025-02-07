using Microsoft.AspNetCore.Mvc;
using FribergCarRental.Services;
using FribergCarRental.Filters;
using FribergCarRental.Models.ViewModel;
using FribergCarRental.data;

namespace FribergCarRental.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AccountController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        // GET: Account/LoginOrRegister
        public IActionResult LoginOrRegister(string returnUrl)
        {
            var accountViewModel = new RegisterLoginViewModel
            {
                LoginViewModel = new LoginViewModel
                {
                    ReturnUrl = returnUrl
                },
                RegisterViewModel = new RegisterViewModel
                {
                    ReturnUrl = returnUrl
                }
            };

            return View(accountViewModel);
        }

        // GET: Account/Login
        public IActionResult Login(string? returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl ?? string.Empty
            };
            return View(model);
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _authService.Login(loginViewModel.Email, loginViewModel.Password))
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Booking");
                    }
                    return Redirect(loginViewModel.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Ogiltigt konto eller lösenord.");
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

        // GET: Account/Profile
        [SimpleAuthorize]
        public async Task<IActionResult> Profile()
        {
            // Get account
            var user = await _authService.GetAccount();
            if (user == null || user.Contact == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            // Only allow unique email
            if (await _authService.Exists(registerViewModel.Email))
            {
                ModelState.AddModelError("", "An account with this email or username already exists.");
            }

            // Handle invalid modelstate
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            // Register new user
            try
            {
                await _authService.Register(registerViewModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occured while registring, please try again.");

                return View(registerViewModel);
            }
            return RedirectToAction("Index", "Booking");
        }

        // GET: Account/AccessDenied
        [Route("AccessDenied")]
        [Route("Account/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SimpleAuthorize]
        public async Task<IActionResult> EditContact(ContactViewModel contactViewModel)
        {
            var account = await _authService.GetAccount();

            // Make sure account was found
            if (account == null || account.Contact == null)
            {
                return NotFound();
            }

            // Make sure it is owned by user
            var contact = account.Contact;
            if (contactViewModel.Id != account.Contact.Id)
            {
                return NotFound();
            }

            // Check modelstate
            if (!ModelState.IsValid)
            {
                return View("Profile", new AccountViewModel { ContactViewModel = contactViewModel });
            }

            // Update fields
            contact.FirstName = contactViewModel.FirstName;
            contact.LastName = contactViewModel.LastName;
            contact.Address = contactViewModel.Address;
            contact.City = contactViewModel.City;
            contact.PostalCode = contactViewModel.PostalCode;
            contact.Phone = contactViewModel.Phone;

            account.Contact = contact;
            await _userRepository.UpdateAsync(account);

            TempData["SuccessMessage"] = "Dina uppgifter har uppdaterats.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SimpleAuthorize]
        public async Task<IActionResult> EditUser(UserViewModel userViewModel)
        {
            // Must be own profile
            if (userViewModel.Id != await _authService.GetCurrentUserId())
            {
                return NotFound();
            }

            // Check modelstate
            if (!ModelState.IsValid)
            {
                return View("Profile", new AccountViewModel { UserViewModel = userViewModel });
            }

            // If password is empty, don't update it
            if (string.IsNullOrEmpty(userViewModel.Password))
            {
                return View("Profile", new AccountViewModel { UserViewModel = userViewModel });
            }

            // Fetch user
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == userViewModel.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Användaren kunde inte hittas.";
                return View("Profile", new AccountViewModel { UserViewModel = userViewModel });
            }

            // Update and save changes
            user.Password = userViewModel.Password;
            await _userRepository.UpdateAsync(user);

            TempData["SuccessMessage"] = "Ditt lösenord har uppdaterats.";
            return RedirectToAction("Profile");
        }
    }
}
