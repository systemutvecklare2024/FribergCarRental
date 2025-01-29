using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.ViewModel
{
    public class BookingViewModel
    {
        public int? Id { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Start datum är obligatoriskt")]
        [Display(Name = "Start datum")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "Slut datum är obligatoriskt")]
        [Display(Name = "Slut datum")]
        public DateOnly EndDate { get; set; }

        public List<SelectListItem>? Cars { get; set; }
        public Dictionary<int, decimal>? CarPrices { get; set; }
    }
}
