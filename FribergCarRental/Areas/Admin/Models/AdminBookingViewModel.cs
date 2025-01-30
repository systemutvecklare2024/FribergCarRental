using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Areas.Admin.Models
{
    public class AdminBookingViewModel : IValidatableObject
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
        public List<SelectListItem>? Users { get; set; }

        /// <summary>
        /// Automate validation of dates and totalcost
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
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
