using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.Entities
{
    public class Receipt
    {
        public int Id { get; set; }

        public Booking Booking { get; set; }

        // Customer information
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        // Car information
        [Required]
        public string LicensePlate { get; set; }

        [Required]
        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        // Booking information
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }

        // Price information

        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }
    }
}
