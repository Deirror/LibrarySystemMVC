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


            List<User> users = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Nickname = "Admin",
                    Password = Crypto.HashPassword("admin123"),
                    Role = "Admin",
                    Email = "admin@gmail.com",
                    Name = "Alex Angelow",
                    IsConfirmed = "Yes"
                },
                new User
                {
                 Id= Guid.NewGuid(),
                 Nickname="Librarian",
                 Password = Crypto.HashPassword("librarian123"),
                 Role="Librarian",
                    Email = "librarian@gmail.com",
                    Name = "Lili",
                    IsConfirmed = "Yes"
                },
                 new User
                 {
                     Id = Guid.NewGuid(),
                     Nickname = "Reader",
                     Password = Crypto.HashPassword("reader123"),
                     Role = "Reader",
                     Email = "reader@gmail.com",
                     Name = "Readercho",
                     IsConfirmed = "Yes"
                 }
            };


            modelBuilder.Entity<User>().HasData(users);


            List<Section> sections = new List<Section>()
            {
                   new Section
                  {
                      Id = Guid.NewGuid(),
                      Name= "Adventure",
                      Description= "This section is about library units which are associated with fun and adrenaline."
                  },

                new Section
                {
                    Id = Guid.NewGuid(),
                    Name = "Fantasy",
                    Description = "This section is about readers who really like using their imagination!"
                },

                 new Section
                 {
                     Id = Guid.NewGuid(),
                     Name = "Horror",
                     Description = "This is for people who are brave!"
                 },

                  new Section
                  {
                      Id = Guid.NewGuid(),
                      Name = "Science",
                      Description = "This section is about people who like learning new interesting stuff."
                  },

                   new Section
                   {
                       Id = Guid.NewGuid(),
                       Name = "Biography",
                       Description = "These types of library units reveal the life of a person."
                   },

                    new Section
                    {
                        Id = Guid.NewGuid(),
                        Name = "Thriller",
                        Description = "This section is a genre of fiction with numerous, often overlapping, subgenres, including crime and detective fiction."
                    }

            };


            modelBuilder.Entity<Section>().HasData(sections);


            List<Title> titles = new List<Title>()
            {

                new Title
                {
                    Id = Guid.NewGuid(),
                    Name="Into the Wild",
                    Description="Into the Wild is a 1996 non-fiction book written by Jon Krakauer. It is an expansion of a 9,000-word article by Krakauer on Chris McCandless titled \"Death of an Innocent\", which appeared in the January 1993 issue of Outside.",
                    Author="Jon Krakauer",
                    Year=1996,
                    ISBN="2123",
                    Type="Art Book",
                    ImageUrl="\\Images\\intothewild.jpg",
                    Publisher="Villard",
                    PublishedYear=1996,
                    SectionId=sections[4].Id
                },


                new Title
                {
                    Id = Guid.NewGuid(),
                    Name="A Brief History of Time",
                    Description="A Brief History of Time: From the Big Bang to Black Holes is a book on theoretical cosmology by English physicist Stephen Hawking",
                    Author="Stephen Hawking",
                    Year=1983,
                    ISBN="2343",
                    Type="TextBook",
                    ImageUrl="\\Images\\BriefHistoryTime.jpg",
                    Publisher="Bantam Dell",
                    PublishedYear=1988,
                    SectionId=sections[3].Id
                },


                new Title
                {
                    Id = Guid.NewGuid(),
                    Name="American Gods",
                    Description="American Gods is a fantasy novel by British author Neil Gaiman. The novel is a blend of Americana, fantasy, and various strands of ancient and modern mythology, all centering on the mysterious and taciturn Shadow",
                    Author="Neil Gaiman",
                    Year=2001,
                    ISBN="2348",
                    Type="Art Book",
                    ImageUrl="\\Images\\American_gods.jpg",
                    Publisher="William Morrow",
                    PublishedYear=2002,
                    SectionId=sections[1].Id
                },

                new Title
                {
                    Id = Guid.NewGuid(),
                    Name="Smile",
                    Description="Smile is a 2022 American psychological supernatural horror film written and directed by Parker Finn",
                    Author="Parker Pinn",
                    Year=2022,
                    ISBN="6666",
                    Type="Film",
                    ImageUrl="\\Images\\Smile_(2022_film).jpg",
                    SectionId=sections[2].Id
                },

                new Title
                {
                    Id = Guid.NewGuid(),
                    Name="National Geographic Adventure",
                    Description="National Geographic Adventure was a magazine started in 1999 by the National Geographic Society in the United States",
                    Author="National Geographic Society",
                    Year=1999,
                    ISBN="4444",
                    Type="Magazine",
                    ImageUrl="\\Images\\National_Geographic_Adventure_December_2009_(cropped).jpg",
                    SectionId=sections[0].Id
                },

                new Title
                {
                    Id = Guid.NewGuid(),
                    Name="The Pink Panther",
                    Description="The Pink Panther is an American media franchise primarily focusing on a series of comedy-mystery films featuring an inept French police detective, Inspector Jacques Clouseau",
                    Author="Blake Edwards",
                    Year=1963,
                    ISBN="3456",
                    Type="Film",
                    ImageUrl="\\Images\\The_Pink_Panther_Passport_to_Peril_Windows_Cover_Art.jpg",
                    SectionId=sections[0].Id
                },

                new Title
                {
                    Id = Guid.NewGuid(),
                    Name="Not a penny more, not a penny less",
                    Description="Not a Penny More, Not a Penny Less was Jeffrey Archer's first novel, first published in 1976",
                    Author="Jeffrey Archer",
                    Year=1976,
                    ISBN="2318",
                    Type="Art Book",
                    ImageUrl="\\Images\\Not_a_Penny_More,_Not_a_Penny_Less_-_Jeffrey_Archer.jpg",
                    Publisher="Bard",
                    PublishedYear=1977,
                    SectionId=sections[5].Id
                },


                new Title
                {
                    Id = Guid.NewGuid(),
                    Name="Pearl Harbor",
                    Description="Pearl Harbor is a 2001 American romantic war drama film directed by Michael Bay, produced by Bay and Jerry Bruckheimer and written by Randall Wallace",
                    Author="Michael Bay",
                    Year=2001,
                    ISBN="3123",
                    Type="Film",
                    ImageUrl="\\Images\\Pearl_harbor_movie_poster.jpg",
                    SectionId=sections[5].Id
                },

                new Title
                {
                    Id = Guid.NewGuid(),
                    Name="Beethoven",
                    Description="Beethoven is a 1992 American family comedy film, directed by Brian Levant and starring Charles Grodin, Bonnie Hunt, Dean Jones, Oliver Platt and Stanley Tucci",
                    Author="Brian Levant",
                    Year=1992,
                    ISBN="3423",
                    Type="Film",
                    ImageUrl="\\Images\\Beethoven'1992.jpg",
                    SectionId=sections[0].Id
                },
            };

            modelBuilder.Entity<Title>().HasData(titles);


            List<LibraryUnit> lus = new List<LibraryUnit>()
            {
                new LibraryUnit
                {
                    Id = Guid.NewGuid(),
                    TitleId=titles[0].Id,
                    CurrentCondition="Good",
                    Medium="Paper",
                    Place="Library Despot",

                },

                new LibraryUnit
                {
                    Id = Guid.NewGuid(),
                    TitleId=titles[1].Id,
                    CurrentCondition="Good",
                    Medium="Paper",
                    Place="Store Orange",

                },

                new LibraryUnit
                {
                    Id = Guid.NewGuid(),
                    TitleId=titles[2].Id,
                    CurrentCondition="Unsuitable",
                    Medium="Paper",
                    Place="Library Despot",

                },

                new LibraryUnit
                {
                    Id = Guid.NewGuid(),
                    TitleId=titles[3].Id,
                    CurrentCondition="Good",
                    Medium="Tape Recorder",
                    Place="Shop TechBuds",

                },

                new LibraryUnit
                {
                    Id = Guid.NewGuid(),
                    TitleId=titles[4].Id,
                    CurrentCondition="Damaged",
                    Medium="Paper",
                    Place="Shop TechBuds",

                },

                new LibraryUnit
                {
                    Id = Guid.NewGuid(),
                    TitleId=titles[5].Id,
                    CurrentCondition="Unsuitable",
                    Medium="Disc",
                    Place="Library Despot",

                },

                new LibraryUnit
                {
                    Id = Guid.NewGuid(),
                    TitleId=titles[6].Id,
                    CurrentCondition="Good",
                    Medium="Paper",
                    Place="Store Orange",

                },

                new LibraryUnit
                {
                    Id = Guid.NewGuid(),
                    TitleId=titles[7].Id,
                    CurrentCondition="Damaged",
                    Medium="Tape Recorder",
                    Place="Library Despot",

                },

                new LibraryUnit
                {
                    Id = Guid.NewGuid(),
                    TitleId=titles[8].Id,
                    CurrentCondition="Good",
                    Medium="Disc",
                    Place="Store Orange",

                }
            };

            modelBuilder.Entity<LibraryUnit>().HasData(lus);

        }

    }
}

