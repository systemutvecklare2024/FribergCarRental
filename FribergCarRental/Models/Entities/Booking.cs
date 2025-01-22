using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FribergCarRental.Models.Entities
{
    public class Booking : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bil obligatorisk")]

        
        [Display(Name = "Bil")]
        public virtual Car Car { get; set; }
        [Required]
        public int CarId {  get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }


        [Required(ErrorMessage = "Startdatum är obligatoriskt")]
        [Display(Name = "Startdatum")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "Slutdatum är obligatoriskt")]
        [Display(Name = "Slutdatum")]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "Total kostnad är obligatoriskt")]
        [Display(Name = "Total kostnad")]
        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

        /// <summary>
        /// Automate validation of dates and totalcost
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(StartDate > EndDate)
            {
                yield return new ValidationResult(
                    "Start datum behöver vara före slut datum.",
                    new[] { nameof(StartDate), nameof(EndDate) });
            }

            if(TotalCost <= 0)
            {
                yield return new ValidationResult(
                    "Kostnad kan ej vara negativ.",
                    new[] {nameof(TotalCost)});
            }
        }
    }
}
