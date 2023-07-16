using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models.ViewModels
{
    public class LoginRequest
    {
        public required string Nickname { get; set; }

        [StringLength(50, ErrorMessage = "The password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
