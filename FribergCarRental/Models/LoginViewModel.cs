using System.ComponentModel.DataAnnotations;

namespace FribergCarRental.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Account { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
