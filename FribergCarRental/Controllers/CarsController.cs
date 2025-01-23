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

        // GET: CarController
        public async Task<IActionResult> Index()
        {
            var cars = await _carRepository.AllAsync();
            return View(cars);
        }

        // GET: CarController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carRepository.GetAsync(id);
            return View(car);
        }
    }
}
