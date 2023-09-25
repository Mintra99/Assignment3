using Assignment3.Exceptionhandler;
using Assignment3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            Console.WriteLine(character);

            if (character == null)
            {
                throw new NotFoundException($"{nameof(Character)} with ID {id} not found");
            }

            return character;
        }

        public async Task<Character> UpdateAsync(Character entity)
        {
            var existingCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == entity.Id);

            if (existingCharacter == null)
            {
                throw new NotFoundException($"{nameof(Character)} with ID {entity.Id} not found");
            }

            // Update the properties of the existing character
            existingCharacter.FullName = entity.FullName;
            existingCharacter.Alias = entity.Alias;
            existingCharacter.Gender = entity.Gender;
            existingCharacter.PictureUrl = entity.PictureUrl;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return existingCharacter;
        }
    }
}
