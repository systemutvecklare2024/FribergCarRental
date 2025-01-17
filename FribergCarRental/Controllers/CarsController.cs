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
        public IActionResult Index()
        {
            var cars = _carRepository.All();
            return View(cars);
        }

        // GET: CarController/Details/5
        public IActionResult Details(int id)
        {
            var car = _carRepository.Get(id);
            return View(car);
        }
    }
}
