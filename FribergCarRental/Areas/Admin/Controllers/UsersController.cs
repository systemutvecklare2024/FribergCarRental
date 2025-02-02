using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergCarRental.Models.Entities;
using FribergCarRental.data;
using FribergCarRental.Filters;
using FribergCarRental.Models.ViewModel;
using FribergCarRental.Services;

namespace FribergCarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SimpleAuthorize(Role = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UsersController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.AllWithBookingsAsync());
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel registerViewModel)
        {
            if (await _authService.Exists(registerViewModel.Email))
            {
                ModelState.AddModelError("Email", "E-postadressen används redan.");
            }

            if (await _userRepository.AnyAsync(u => u.Username == registerViewModel.Username))
            {
                ModelState.AddModelError("Username", "Användarnamnet används redan.");
            }

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            await _authService.Register(registerViewModel);

            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,Password,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userRepository.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository
                .GetWithBookingsAsync(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            if (id == 1 || user.HasBookings())
            {
                // Add a toast?
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 1)
            {
                // Add toast?
                return RedirectToAction(nameof(Index));
            }

            var user = await _userRepository.GetAsync(id);
            if (user != null)
            {
                await _userRepository.RemoveAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExists(int id)
        {
            return await _userRepository.AnyAsync(e => e.Id == id);
        }
    }
}
