using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibrarysSystem.Models.ViewModels
{
    public class LoginRequest
    {
        [Required]
        public string Nickname { get; set; }

        [Required] 
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
