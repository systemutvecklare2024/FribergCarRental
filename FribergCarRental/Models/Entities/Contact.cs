using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.Entities
{
    public class Contact : IEntity
    {
        // Relations
        public int UserId { get; set; }

        // Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Förnamn är obligatoriskt")]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Adress är obligatorisk")]
        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ort är obligatoriskt")]
        [Display(Name = "Ort")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postnummer är obligatoriskt")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postnummer")]
        public string PostalCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Telefonnummer är obligatoriskt")]
        [Display(Name = "Telefonnummer")]
        public string Phone { get; set; }

        // Navigation
        public virtual User? User { get; set; }
    }
}
