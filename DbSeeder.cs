using Microsoft.EntityFrameworkCore;

namespace Assignment3
{
    public class DbSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new MovieDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MovieDbContext>>()))
            {
                // Check if the database is already seeded
                if (context.Characters.Any() || context.Movies.Any() || context.Franchises.Any())
                {
                    return; // Database has already been seeded
                }

                // Seed sample data
                var franchise1 = new Franchise
                {
                    Name = "Marvel Cinematic Universe",
                    Description = "A cinematic universe of superhero films"
                };

                var franchise2 = new Franchise
                {
                    Name = "The Lord of the Rings",
                    Description = "A high-fantasy epic film series"
                };

                var franchise3 = new Franchise
                {
                    Name = "The Dark Knight Series",
                    Description = "A set of three Christopher Nolan Batman movies"
                };

                context.Franchises.AddRange(franchise1, franchise2,  franchise3);

                var movie1 = new Movie
                {
                    MovieTitle = "Iron Man",
                    Genre = "Action, Adventure, Sci-Fi",
                    ReleaseYear = 2008,
                    Director = "Jon Favreau",
                    PictureUrl = "https://example.com/ironman-poster.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=8ugaeA-nMTc",
                    Franchise = franchise1
                };

                var movie2 = new Movie
                {
                    MovieTitle = "The Lord of the Rings: The Fellowship of the Ring",
                    Genre = "Action, Adventure, Drama",
                    ReleaseYear = 2001,
                    Director = "Peter Jackson",
                    PictureUrl = "https://example.com/lotr-poster.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=_nZdmwHrcnw",
                    Franchise = franchise2
                };

                var movie3 = new Movie
                {
                    MovieTitle = "The Dark Knight",
                    Genre = "Action, Drama, Thriller, Crime",
                    ReleaseYear = 2008,
                    Director = "Christopher Nolan",
                    PictureUrl = "https://example.com/thedarkknight-poster.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=UXmBT9JtuLo",
                    Franchise = franchise3
                };

                context.Movies.AddRange(movie1, movie2);

                var character1 = new Character
                {
                    FullName = "Tony Stark",
                    Alias = "Iron Man",
                    Gender = "Male",
                    PictureUrl = "https://example.com/tony-stark.jpg",
                    Movies = new List<Movie> { movie1 }
                };

                var character2 = new Character
                {
                    FullName = "Frodo Baggins",
                    Alias = null,
                    Gender = "Male",
                    PictureUrl = "https://example.com/frodo-baggins.jpg",
                    Movies = new List<Movie> { movie2 }
                };

                var character3 = new Character
                {
                    FullName = "Bruce Wayne",
                    Alias = "Batman",
                    Gender = "Male",
                    PictureUrl = "https://example.com/bruce-wayne.jpg",
                    Movies = new List<Movie> { movie3 }
                };

                context.Characters.AddRange(character1, character2,  character3);

                context.SaveChanges();
            }
        }
    }
}
