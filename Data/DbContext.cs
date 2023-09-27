using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Assignment3.Models
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        public MovieDbContext()
        {
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many relationship between Franchise and Movie
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Franchise)
                .WithMany(f => f.Movies)
                .HasForeignKey(m => m.FranchiseId)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed data
            modelBuilder.Entity<Franchise>().HasData(
                new Franchise()
                {
                    Id = 1,
                    Name = "Marvel Cinematic Universe",
                    Description = "A cinematic universe of superhero films"
                },
                new Franchise()
                {
                    Id = 2,
                    Name = "The Lord of the Rings",
                    Description = "A high-fantasy epic film series"
                },
                new Franchise()
                {
                    Id = 3,
                    Name = "The Dark Knight Series",
                    Description = "A set of three Christopher Nolan Batman movies"
                }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 1,
                    MovieTitle = "Iron Man",
                    Genre = "Action, Adventure, Sci-Fi",
                    ReleaseYear = 2008,
                    Director = "Jon Favreau",
                    PictureUrl = "https://example.com/ironman-poster.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=8ugaeA-nMTc",
                    FranchiseId = 1 // Marvel Cinematic Universe
                },
                new Movie()
                {
                    Id = 2,
                    MovieTitle = "The Lord of the Rings: The Fellowship of the Ring",
                    Genre = "Action, Adventure, Drama",
                    ReleaseYear = 2001,
                    Director = "Peter Jackson",
                    PictureUrl = "https://example.com/lotr-poster.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=_nZdmwHrcnw",
                    FranchiseId = 2 // The Lord of the Rings
                },
                new Movie()
                {
                    Id = 3,
                    MovieTitle = "The Dark Knight",
                    Genre = "Action, Drama, Thriller, Crime",
                    ReleaseYear = 2008,
                    Director = "Christopher Nolan",
                    PictureUrl = "https://example.com/thedarkknight-poster.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=UXmBT9JtuLo",
                    FranchiseId = 3 // The Dark Knight Series
                }
            );

            modelBuilder.Entity<Character>().HasData(
                new Character()
                {
                    Id = 1,
                    FullName = "Tony Stark",
                    Alias = "Iron Man",
                    Gender = "Male",
                    PictureUrl = "https://example.com/tony-stark.jpg",
                },
                new Character()
                {
                    Id = 2,
                    FullName = "Frodo Baggins",
                    Alias = null,
                    Gender = "Male",
                    PictureUrl = "https://example.com/frodo-baggins.jpg",
                },
                new Character()
                {
                    Id = 3,
                    FullName = "Bruce Wayne",
                    Alias = "Batman",
                    Gender = "Male",
                    PictureUrl = "https://example.com/bruce-wayne.jpg",
                }
            );

            // Establish many-to-many relationship between Movie and Character
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Characters)
                .WithMany(c => c.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieCharacter",
                    l => l.HasOne<Character>().WithMany().HasForeignKey("CharacterId").OnDelete(DeleteBehavior.SetNull),
                    r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.SetNull),
                    je =>
                    {
                        je.HasKey("MovieId", "CharacterId");
                        je.HasData(
                                    new
                                    {
                                        MovieId = 1, // Iron Man
                                        CharacterId = 1 // Tony Stark (Iron Man)
                                    },
                                    new
                                    {
                                        MovieId = 2, // The Lord of the Rings: The Fellowship of the Ring
                                        CharacterId = 2 // Frodo Baggins
                                    },
                                    new
                                    {
                                        MovieId = 3, // The Dark Knight
                                        CharacterId = 3 // Bruce Wayne (Batman)
                                    }
                            );
                    }
                );

        }
    }
}
