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
            var franchises = await _db.Franchises.ToListAsync();

            return franchises;
        }

        public Task<Franchise> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Franchise> UpdateAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }
    }
}
