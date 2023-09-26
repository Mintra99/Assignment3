using Assignment3.Exceptionhandler;
using Assignment3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Services.Franchises
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MovieDbContext _db;
        public FranchiseService(MovieDbContext db) {
            _db = db;
        }

        public async Task<Franchise> CreateAsync(Franchise entity)
        {
            try
            {
                await _db.Franchises.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Franchise>> GetAsync()
        {
            var franchises = await _db.Franchises
                .Include(f => f.Movies)
                .ToListAsync();

            return franchises;
        }

        public async Task<Franchise> GetByIdAsync(int id)
        {

            if(!await FranchiseExistsAsync(id))
            {
                throw new EntityNotFoundException("Franchise", id);
            }

            try
            {
                var franchise = await _db.Franchises
                    .Include(f => f.Movies)
                    .SingleOrDefaultAsync(f => f.Id == id);

                return franchise!;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Franchise> UpdateAsync(Franchise entity)
        {
            if (!await FranchiseExistsAsync(entity.Id))
            {
                throw new EntityNotFoundException("Franchise", entity.Id);
            }

            try
            {
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (SqlException err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public async Task<Franchise> DeleteAsync(int id)
        {
            if (!await FranchiseExistsAsync(id))
            {
                throw new EntityNotFoundException("Franchise", id);
            }

            try
            {
                var franchiseToDelete = await _db.Franchises.SingleAsync(f => f.Id == id);

                _db.Remove(franchiseToDelete);
                await _db.SaveChangesAsync();
                return franchiseToDelete;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Movie>> GetMoviesAsync(int id)
        {
            if (!await FranchiseExistsAsync(id))
            {
                throw new EntityNotFoundException("Franchise", id);
            }

            try
            {
                var movies = await _db.Movies
                    .Where(m => m.FranchiseId == id)
                    .Include(m => m.Characters)
                    .ToListAsync();

                    return movies;
            }
            catch (SqlException err)
            {
                Console.WriteLine(err.Message);
                
                throw;
            }
        }

        public async Task UpdateMoviesAsync(int id, int[] movies)
        {
            if(!await FranchiseExistsAsync(id))
            {
                throw new EntityNotFoundException("Franchise", id);
            }

            List<Movie> movieList = new List<Movie>();

            foreach (int movieId in movies)
            {
                if (!await MovieExistsAsync(movieId))
                {
                    throw new EntityNotFoundException("Movie", movieId);
                }
                
                movieList.Add(await _db.Movies.SingleAsync(m => m.Id == movieId));
                
            }

            var franchisToUpdate = await _db.Franchises
                .Include(f => f.Movies)
                .SingleOrDefaultAsync(f => f.Id == id);

            franchisToUpdate!.Movies = movieList;
            await _db.SaveChangesAsync();
        }

        public async Task<List<Character>> GetCharactersAsync(int id)
        {
            if (!await FranchiseExistsAsync(id))
            {
                throw new EntityNotFoundException("Franchise", id);
            }

            List<Character> characters = new List<Character>();

            var movies = await _db.Movies
                .Include(m => m.Characters)
                .Where(m => m.FranchiseId == id)
                .ToListAsync();


            foreach( var movie in movies)
            {
                foreach (var character in movie.Characters!)
                {
                    characters.Add(character);
                }
            }
            
            return characters;
        }

        public async Task<bool> FranchiseExistsAsync(int id)
        {
            return await _db.Franchises.AnyAsync(f => f.Id == id);
        }
        public async Task<bool> MovieExistsAsync(int id)
        {
            return await _db.Movies.AnyAsync(f => f.Id == id);
        }

    }
}
