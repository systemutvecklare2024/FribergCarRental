using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.ViewModel
{
    public class BookingIndexViewModel
    {
        public int Id { get; set;
        }
        [Display(Name = "Bil")]
        public string CarModel { get; set; }

        [Display(Name = "Startdatum")]
        public DateOnly StartDate { get; set; }

        [Display(Name = "Slut datum")]
        public DateOnly EndDate { get; set; }

        [Display(Name = "Total kostnad")]
        public decimal TotalCost {  get; set; }

        public bool IsUpcoming { get; set; }
    }
}
