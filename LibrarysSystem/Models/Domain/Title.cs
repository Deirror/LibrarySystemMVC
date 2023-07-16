using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarysSystem.Models.Domain
{
    public class Title
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string Author { get; set; }

        public int Year { get; set; }

        public string? ISBN { get; set; }

        public required string Type { get; set; }

        public required string ImageUrl { get; set; }

        public  string? Publisher { get; set; }

        public int? PublishedYear { get; set; }

        public required Section Section { get; set; }



    }
}
