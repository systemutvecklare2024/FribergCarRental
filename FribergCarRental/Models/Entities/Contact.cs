using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.Entities
{
    public class Contact : IEntity
    {
        // Relations
        public int UserId { get; set; }

        // Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        // Navigation
        public virtual User? User { get; set; }
    }
}
