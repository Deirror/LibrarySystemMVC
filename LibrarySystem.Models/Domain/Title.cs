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
    public class Title
    {

        public Guid Id { get; set; }
        [StringLength(maximumLength: 40, ErrorMessage = "The name must be at least 3 characters long.", MinimumLength = 3)]
        public required string Name { get; set; }
        [StringLength(maximumLength: 300, ErrorMessage = "The description must be at least 10 characters long.", MinimumLength = 10)]
        public required string Description { get; set; }

        public required string Author { get; set; }
        [Range(1700,2023,ErrorMessage="Must be between 1700 and 2023!")]
        public int Year { get; set; }

        public string? ISBN { get; set; }

        public required string Type { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }

        public string? Publisher { get; set; }

        [DisplayName("Published Year")]
        public int? PublishedYear { get; set; }

        public Section? Section { get; set; }
    }
}
