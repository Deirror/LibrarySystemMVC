namespace LibrarysSystem.Models.Domain
{
    public class LibraryUnit
    {
        public Guid Id { get; set; }

        public Title Title { get; set; }

        public string CurrentCondition { get; set; }

        public string Medium { get; set;}

        public string Place { get; set;}
    }
}
