using Assignment3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


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
            try
            {
                await _context.Characters.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Character>> GetAsync()
        {
            var characters = await _context.Characters
                .Include(c => c.Movies)
                .ToListAsync();

            return characters;
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            try
            {
                var character = _context.Characters
                    .Include(c => c.Movies)
                    .SingleOrDefaultAsync(c => c.Id == id);

                return await character!;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Character> UpdateAsync(Character entity)
        {
            try
            {
                var character = await _context.Characters.SingleOrDefaultAsync(c => c.Id == entity.Id);
                if (character == null)
                {
                    return null!;
                }
                else
                {
                    character!.FullName = entity.FullName;
                    character!.Alias = entity.Alias;
                    character!.Gender = entity.Gender;
                    character!.PictureUrl = entity.PictureUrl;
                    await _context.SaveChangesAsync();
                    return character!;
                }
            }
            catch (SqlException err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public async  Task<Character> DeleteAsync(int id)
        {
            try
            {
                var characterToDelete = await _context.Characters.SingleOrDefaultAsync(c => c.Id == id);

                if (characterToDelete == null)
                {
                    return null!;
                }
                else
                {
                    _context.Remove(characterToDelete);
                    await _context.SaveChangesAsync();
                    return characterToDelete;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
