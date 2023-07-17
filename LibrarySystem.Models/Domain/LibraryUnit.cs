using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models.Domain
{
    public class LibraryUnit
    {
        public Guid Id { get; set; }

        public Guid TitleId { get; set; }
        [ValidateNever]
        public Title? Title { get; set; }

        public required string CurrentCondition { get; set; }

        public required string Medium { get; set; }

        public required string Place { get; set; }
    }
}
