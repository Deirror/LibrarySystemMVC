using System.ComponentModel.DataAnnotations;

namespace LibrarysSystem.Models.ViewModels
{
    public class RegisterRequest
    {
        [Required]
        public string Nickname { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
