using Assignment3.Helpers.Exceptions;
using Assignment3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _db;
        public MovieService(MovieDbContext db)
        {
            _db = db;
        }

        public async Task<Movie> CreateAsync(Movie entity)
        {
            try
            {
                await _db.Movies.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Movie>> GetAsync()
        {
            var movies = await _db.Movies
                .Include(m => m.Characters)
                .ToListAsync();

            return movies;
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            try
            {
                var movie = _db.Movies
                    .Include(m => m.Characters)
                    .SingleOrDefaultAsync(m => m.Id == id);

                return movie!;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Movie> UpdateAsync(Movie entity)
        {
            try
            {
                var movie = await _db.Movies.AsNoTracking().SingleOrDefaultAsync(m => m.Id == entity.Id);
                if (movie == null)
                {
                    return null!;
                }
                else
                {
                    _db.Entry(entity).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return movie!;
                }
            }
            catch (SqlException err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public async Task<Movie> DeleteAsync(int id)
        {
            try
            {
                var movieToDelete = await _db.Movies.SingleOrDefaultAsync(f => f.Id == id);

                if (movieToDelete == null)
                {
                    return null!;
                }
                else
                {
                    _db.Remove(movieToDelete);
                    await _db.SaveChangesAsync();
                    return movieToDelete;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<ICollection<Character>> GetCharactersAsync(int id)
        {
            if (!await MovieExistsAsync(id))
            {
                throw new EntityNotFoundException("Movie", id);
            }

            List<Character> characters = new List<Character>();

            var movies = await _db.Movies
                .Include(m => m.Characters)
                .Where(m => m.FranchiseId == id)
                .ToListAsync();


            foreach (var movie in movies)
            {
                foreach (var character in movie.Characters!)
                {
                    if (!characters.Contains(character))
                    {
                        characters.Add(character);
                    }
                }
            }

            return characters;
        }

        public async Task UpdateCharactersAsync(int id, int[] characterIds)
        {
            if (!await MovieExistsAsync(id))
            {
                throw new EntityNotFoundException("Movie", id);
            }

            List<Character> charList = new List<Character>();
            foreach (var cid in characterIds)
            { 
                if(!await CharacterExistsAsync(cid))
                {
                    throw new EntityNotFoundException("Character", cid);
                }

                charList.Add(_db.Characters.Single(c => c.Id == cid));
            }

            var movieToUpdate = await _db.Movies.Include(m => m.Characters).SingleAsync(m => m.Id == id);
            movieToUpdate.Characters = charList;

            await _db.SaveChangesAsync();
        }

        // Helper functions

        /// <summary>
        /// Checks if a character with the specified id exists in the database
        /// </summary>
        /// <param name="id">The id of the character to check</param>
        /// <returns><c>true</c> if a character with the specified id exists; otherwise, <c>false</c></returns>
        public async Task<bool> CharacterExistsAsync(int id)
        {
            return await _db.Characters.AnyAsync(f => f.Id == id);
        }

        /// <summary>
        /// Checks if a movie with the specified id exists in the database
        /// </summary>
        /// <param name="id">The id of the movie to check</param>
        /// <returns><c>true</c> if a movie with the specified id exists; otherwise, <c>false</c></returns>
        public async Task<bool> MovieExistsAsync(int id)
        {
            return await _db.Movies.AnyAsync(f => f.Id == id);
        }
    }
}
