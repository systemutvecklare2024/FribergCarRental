using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.Entities
{
    public class Car : IEntity
    {
        // Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [Display( Name = "Modell")]
        public string Model { get; set; }

        [Required(ErrorMessage = "ImageUrl is required")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal PricePerDay { get; set; }

        // Navigation
        public ICollection<Booking>? Bookings { get; set; }
    }
}