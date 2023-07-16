using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Nickname { get; set; }

        [StringLength(maximumLength: 400, ErrorMessage = "The password must be at least 6 characters long.", MinimumLength = 6)]
        public required string Password { get; set; }

        [ValidateNever]
        public required string Role { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }

        public required string Name { get; set; }
        [DisplayName("Confirmed?")]

        [ValidateNever]
        public required string IsConfirmed{get; set;}
    }
}
