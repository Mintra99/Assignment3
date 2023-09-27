using Assignment3.Exceptionhandler;
using Assignment3.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            var movie = await GetByIdAsync(id);

            if (movie == null)
            {
                throw new EntityNotFoundException(nameof(Movie), id);
            }

            // return movie.Characters.ToList();
            return await _db.Characters
                .Where(c => c.Id == id)
                .ToListAsync();

        }

    }
}
