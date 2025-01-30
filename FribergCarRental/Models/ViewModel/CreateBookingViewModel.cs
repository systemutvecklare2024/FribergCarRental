using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.ViewModel
{
    public class CreateBookingViewModel : IValidatableObject
    {
        [Display(Name = "Bil")]
        public string? CarModel { get; set; }

        [Required(ErrorMessage = "Startdatum är obligatoriskt")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Slutdatum är obligatoriskt.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int CarId { get; set; }

        public decimal? CarPrice { get; set; }

        public decimal? TotalCost { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult(
                    "Start datum behöver vara före slut datum.",
                    new[] { nameof(StartDate), nameof(EndDate) });
            }
        }
    }
}
