using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }
        [Required(ErrorMessage = "NumberOfSeats is required")]
        public int NumberOfSeats { get; set; }
        
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Precision(5,2)]
        public decimal Price { get; set; }
    }
}