using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.Entities
{
    public class Car : IEntity
    {
        // Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Modell är obligatorisk")]
        [Display( Name = "Modell")]
        public string Model { get; set; }

        [Required(ErrorMessage = "ImageUrl är obligatorisk")]
        [Display( Name = "Bild länk")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Pris är obligatoriskt")]
        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        [Display(Name = "Dygnskostnad")]
        public decimal PricePerDay { get; set; }

        // Navigation
        public ICollection<Booking>? Bookings { get; set; }

        public bool HasBookings()
        {
            return Bookings?.Count > 0;
        }
    }
}