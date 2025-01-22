using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models.Entities
{
    [Index(nameof(Username), nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        
        public virtual Contact Contact { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        [Required(ErrorMessage = "Användarnamn är obligatoriskt")]
        [Display(Name = "Användarnamn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "E-postadress är obligatoriskt")]
        [Display(Name = "E-postadress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Display(Name = "Roll")]
        public string Role { get; set; } = "Customer";
    }
}
