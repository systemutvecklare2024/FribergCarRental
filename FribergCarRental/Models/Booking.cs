using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FribergCarRental.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }

        [ForeignKey("ReceiptId")]
        public Receipt Receipt { get;set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public DateOnly EndDate { get; set; }
    }
}
