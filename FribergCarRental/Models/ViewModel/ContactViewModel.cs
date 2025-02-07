using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.ViewModel
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required( ErrorMessage = "Förnamn är obligatoriskt")]
        [Display( Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gatuadress är obligatoriskt")]
        [Display(Name = "Gatuadress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ort är obligatoriskt")]
        [Display(Name = "Ort")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postnummer är obligatoriskt")]
        [Display(Name = "Postnummer")]
        public string PostalCode { get; set; }

        [Required( ErrorMessage = "Telefonnummer är obligatoriskt")]
        [Display(Name = "Telefonnummer")]
        public string Phone { get; set; }
    }
}
