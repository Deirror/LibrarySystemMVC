namespace LibrarysSystem.Models.Domain
{
    public class Section
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Title> Titles { get; set;}
    }
}
