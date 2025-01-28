using FribergCarRental.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Areas.Admin.Models
{
    public class AdminCreateBookingViewModel
    {
        public int CarId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Startdatum är obligatoriskt")]
        [Display(Name = "Startdatum")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "Slutdatum är obligatoriskt")]
        [Display(Name = "Slutdatum")]
        public DateOnly EndDate { get; set; }

        public List<SelectListItem>? Cars { get; set; }
        public Dictionary<int, decimal>? CarPrices { get; set; }
        public List<SelectListItem>? Users { get; set; }

    }
}
