namespace LibrarysSystem.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        public string Nickname { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
