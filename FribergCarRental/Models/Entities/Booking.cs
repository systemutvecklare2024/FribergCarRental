using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.Entities
{
    public class Booking : IValidatableObject, IEntity
    {
        // Relations
        [Required(ErrorMessage = "Bil obligatorisk")]
        public int CarId {  get; set; }

        // Properties
        public int Id { get; set; }
        [Required(ErrorMessage = "Användare är obligatorisk")]
        public int UserId { get; set; }

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

        // Navigation
        [Display(Name = "Bil")]
        public virtual Car? Car { get; set; }

        [Display(Name = "Användare")]
        public virtual User? User { get; set; }

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
