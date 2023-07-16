using System.ComponentModel.DataAnnotations;

namespace LibrarysSystem.Models.Domain
{
    public class MovementLibraryUnit
    {
        public Guid Id { get; set; }
        public Title Title { get; set; }

        public DateTime Date { get; set; }

        public DateTime? TimeLimit { get; set; }

        public string Type { get; set; }

        public User? Reader { get; set; }

        public User? Librarian { get; set; }

        public string Status { get; set; }
    }
}
