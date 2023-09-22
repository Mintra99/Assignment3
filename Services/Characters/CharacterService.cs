using Assignment3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment3.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly MovieDbContext _context;

        public CharacterService(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<Character> CreateAsync(Character entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Characters.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Character>> GetAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            return await _context.Characters.FindAsync(id);
        }

        public async Task<Character> UpdateAsync(Character entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
