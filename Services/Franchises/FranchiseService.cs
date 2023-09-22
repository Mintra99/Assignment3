using Assignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Services.Franchises
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MovieDbContext _db;
        public FranchiseService(MovieDbContext db) {
            _db = db;
        }

        public Task<Franchise> CreateAsync(Franchise entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Franchise>> GetAsync()
        {
            var franchises = _db.Franchises.ToListAsync();

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
