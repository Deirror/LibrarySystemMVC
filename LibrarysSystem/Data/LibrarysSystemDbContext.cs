using LibrarysSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace LibrarysSystem.Data
{
    public class LibrarysSystemDbContext:DbContext
    {
        public LibrarysSystemDbContext(DbContextOptions options):base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<LibraryUnit> LibraryUnits { get; set; }

        public DbSet<MovementLibraryUnit> Movements { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Title> Titles { get; set; }
      
    }
}
