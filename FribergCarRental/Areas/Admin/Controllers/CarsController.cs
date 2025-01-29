using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergCarRental.data;
using FribergCarRental.Filters;
using FribergCarRental.Models.Entities;

namespace FribergCarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SimpleAuthorize( Role = "Admin")]
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // GET: Admin/Cars
        public async Task<IActionResult> Index()
        {
            return View(await _carRepository.AllAsync());
        }

        // GET: Admin/Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Admin/Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,ImageUrl,PricePerDay")] Car car)
        {
            if (ModelState.IsValid)
            {
                await _carRepository.AddAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Admin/Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Admin/Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,ImageUrl,PricePerDay")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _carRepository.UpdateAsync(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarExists(car.Id))
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
            return View(car);
        }

        // GET: Admin/Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Admin/Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _carRepository.GetAsync(id);
            if (car != null)
            {
                await _carRepository.RemoveAsync(car);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarExists(int id)
        {
            return await _carRepository.AnyAsync(e => e.Id == id);
        }
    }
}
