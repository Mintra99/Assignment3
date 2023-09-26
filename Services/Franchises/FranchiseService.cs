﻿using Assignment3.Models;
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

        public Task<Franchise> GetByIdAsync(int id)
        {
            try
            {
                var franchise = _db.Franchises
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
            try
            {
                var franchise = await _db.Franchises.SingleOrDefaultAsync(f => f.Id == entity.Id);
                if (franchise == null)
                {
                    return null!;
                } 
                else
                {
                    franchise!.Name = entity.Name;
                    franchise!.Description = entity.Description;
                    await _db.SaveChangesAsync();
                    return franchise!;
                }
            }
            catch (SqlException err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public async Task<Franchise> DeleteAsync(int id)
        {
            try
            {
                var franchiseToDelete = await _db.Franchises.SingleOrDefaultAsync(f => f.Id == id);

                if(franchiseToDelete == null)
                {
                    return null!;
                }
                else
                {
                    _db.Remove(franchiseToDelete);
                    await _db.SaveChangesAsync();
                    return franchiseToDelete;
                }
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
                return null!;
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

        public async Task<bool> FranchiseExistsAsync(int id)
        {
            return await _db.Franchises.AnyAsync(f => f.Id == id);
        }
    }
}
