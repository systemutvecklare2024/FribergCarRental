using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.ViewModel
{
    public class CreateBookingViewModel
    {
        [Required(ErrorMessage = "Startdatum är obligatoriskt")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Slutdatum är obligatoriskt.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int CarId { get; set; }

        public int ContactId { get; set; }

        public decimal TotalCost {  get; set; }

        // Add these properties for the formatted date values
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
    }
}
