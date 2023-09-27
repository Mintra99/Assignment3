﻿using Assignment3.Models;
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

        /// <summary>
        /// Creates a new character entity in the database.
        /// </summary>
        /// <param name="entity">The character entity to create.</param>
        /// <returns>The created character entity.</returns>
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

        /// <summary>
        /// Retrieves a list of all character entities from the database.
        /// </summary>
        /// <returns>A list of character entities.</returns>
        public async Task<List<Character>> GetAsync()
        {
            var characters = await _context.Characters
                .Include(c => c.Movies)
                .ToListAsync();

            return characters;
        }

        /// <summary>
        /// Retrieves a character entity by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the character entity to retrieve.</param>
        /// <returns>The character entity with the specified ID.</returns>
        public Task<Character> GetByIdAsync(int id)
        {
            try
            {
                var character = _context.Characters
                    .Include(c => c.Movies)
                    .SingleOrDefaultAsync(c => c.Id == id);

                return character!;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing character entity in the database.
        /// </summary>
        /// <param name="entity">The updated character entity.</param>
        /// <returns>The updated character entity.</returns>
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

        /// <summary>
        /// Deletes a character entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the character entity to delete.</param>
        /// <returns>The deleted character entity, or null if not found.</returns>
        public async  Task<Character> DeleteAsync(int id)
        {
            try
            {
                var characterToDelete = await _context.Characters
                    .SingleOrDefaultAsync(c => c.Id == id);

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
