using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FribergCarRental.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public DateOnly EndDate { get; set; }

        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }
    }
}
