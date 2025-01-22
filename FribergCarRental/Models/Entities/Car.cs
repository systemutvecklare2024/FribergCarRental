using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "ImageUrl is required")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal PricePerDay { get; set; }
    }
}