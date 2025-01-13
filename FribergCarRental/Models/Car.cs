using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Brand is required")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Color is required")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Precision(10,2)]
        public decimal Cost { get; set; }
    }
}