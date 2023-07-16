using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models.Domain
{
    public class Section
    {
        public Guid Id { get; set; }
        [StringLength(maximumLength: 40, ErrorMessage = "The name must be at least 3 characters long.", MinimumLength = 3)]
        public required string Name { get; set; }
        [StringLength(maximumLength: 300, ErrorMessage = "The description must be at least 10 characters long.", MinimumLength = 10)]
        public required string Description { get; set; }
        [ValidateNever]
        public required ICollection<Title> Titles { get; set; }
    }
}
