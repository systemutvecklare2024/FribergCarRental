using FribergCarRental.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRental.Areas.Admin.Controllers
{
    [SimpleAuthorize( Role = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        //GET: Admin
        public IActionResult Index()
        {
            return View();
        }
    }
}
