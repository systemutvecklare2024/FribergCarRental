using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.ViewModel
{
    public class UserViewModel : IValidatableObject
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "E-postadress är obligatorisk")]
        //[DataType(DataType.EmailAddress)]
        [Display( Name = "E-postadress")]
        public string? Email { get; set; }

        [Display(Name = "Användarnamn")]
        public string? Username { get; set; }
        [Display(Name = "Lösenord")]
        public string? Password { get; set; }

        [Display(Name = "Upprepa lösenord")]
        public string? PasswordConfirmed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Password && !PasswordConfirmed
            if(!string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(PasswordConfirmed)) {
                yield return new ValidationResult(
                    "Upprepat lösenord krävs",
                    new[] { nameof(PasswordConfirmed) });
            }

            // Password != PasswordConfirmed
            if(!string.IsNullOrEmpty(Password) 
                && !string.IsNullOrEmpty(PasswordConfirmed)
                && Password != PasswordConfirmed)
            {
                yield return new ValidationResult(
                    "Lösenord måste matcha",
                    new[] { nameof(Password) });
            }

            if(Password?.Length < 8)
            {
                yield return new ValidationResult(
                    "Lösenord måste minst vara 8 bokstäver/symboler långt",
                    new[] { nameof(Password) });
            }
        }
    }
}
