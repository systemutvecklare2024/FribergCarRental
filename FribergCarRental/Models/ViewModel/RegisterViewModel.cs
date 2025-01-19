using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Användarnamn är obligatoriskt")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email är obligatoriskt")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Lösenord måste minst vara 8 bokstäver/symboler långt")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Upprepat lösenord är obligatoriskt")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lösenord måste matcha")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Förnamn är obligatoriskt")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Adress är obligatoriskt")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ort är obligatoriskt")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postnummer är obligatoriskt")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Telefonnummer är obligatoriskt")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
