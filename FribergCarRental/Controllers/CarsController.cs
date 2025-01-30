using FribergCarRental.data;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRental.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            var cars = await _carRepository.AllAsync();
            return View(cars);
        }
    }
}
