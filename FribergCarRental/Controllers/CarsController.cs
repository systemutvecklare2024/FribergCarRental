using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergCarRental.Models;
using FribergCarRental.data;
using FribergCarRental.Filters;

namespace FribergCarRental.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _carRepository.ToListAsync());
        }

        // GET: Cars/Details/5
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

        // GET: Cars/Create
        [SimpleAuthorize(Role = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SimpleAuthorize(Role = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Model,ImageUrl,Cost")] Car car)
        {
            if (ModelState.IsValid)
            {
                _carRepository.Add(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        [SimpleAuthorize(Role = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SimpleAuthorize(Role = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,ImageUrl,Cost")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _carRepository.Update(car);
                    await _carRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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

        // GET: Cars/Delete/5
        [SimpleAuthorize(Role = "Admin")]
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

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SimpleAuthorize(Role = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _carRepository.FindAsync(id);
            if (car != null)
            {
                _carRepository.Remove(car);
            }

            await _carRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _carRepository.Any(e => e.Id == id);
        }
    }
}
