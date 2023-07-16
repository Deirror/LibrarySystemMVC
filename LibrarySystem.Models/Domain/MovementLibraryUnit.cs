using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models.Domain
{
    public class MovementLibraryUnit
    {
        public Guid Id { get; set; }
        public required Title Title { get; set; }

        public DateTime Date { get; set; }

        [DisplayName("Time Limit")]
        public DateTime? TimeLimit { get; set; }

        public required string Type { get; set; }
        [DisplayName("Reader's Nickname")]
        public User? Reader { get; set; }
        [DisplayName("Librarian's Nickname")]
        public User? Librarian { get; set; }

        public required string Status { get; set; }

    }
}
