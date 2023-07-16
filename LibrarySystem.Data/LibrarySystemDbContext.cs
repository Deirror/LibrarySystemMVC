using LibrarySystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace LibrarySystem.Data
{
    public class LibrarySystemDbContext:DbContext
    {
        public LibrarySystemDbContext(DbContextOptions<LibrarySystemDbContext> options):base(options)
        {
         
        }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Title> Titles { get; set; }    
        public DbSet<LibraryUnit> LibraryUnits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovementLibraryUnit> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Nickname).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Section>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Title>().HasIndex(x => x.Name).IsUnique();


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Nickname = "Admin",
                    Password = Crypto.HashPassword("admin123"),
                    Role = "Admin",
                    Email = "admin@gmail.com",
                    Name = "Alex Angelow",
                    IsConfirmed = "Yes"
                }
             );

         
        }

    }
}
