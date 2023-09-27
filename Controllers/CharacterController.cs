using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Models;
using AutoMapper;
using Assignment3.Data.Dtos.Characters;
using Assignment3.Services.Characters;
using Assignment3.Data.Dtos.Franchises;
using Assignment3.Helpers;
using Assignment3.Helpers.Exceptions;

namespace Assignment3.Controllers
{
    [Route("api/v1/Character")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    public class CharacterController : ControllerBase
    {
        // Want to sparate out our concerns
        // This lets us keep data access, business logic, mapping and user interaction separate
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all characters.
        /// </summary>
        /// <returns>A list of character objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters()
        {
            var characters = await _characterService.GetAsync();
            return Ok(_mapper.Map<IEnumerable<CharacterDto>>(characters));

        }

        /// <summary>
        /// Retrieves a character by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the character.</param>
        /// <returns>The character object with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> GetCharacter(int id)
        {
            var character = await _characterService.GetByIdAsync(id);
            if (character == null)
            {
                var notFoundResponse = new NotFoundResponse($"Character with ID {id} not found.");
                return NotFound(notFoundResponse);
            }
            
            return Ok(_mapper.Map<CharacterDto>(character));

        }

        /// <summary>
        /// Updates an existing character's information.
        /// </summary>
        /// <param name="id">The unique identifier of the character to update.</param>
        /// <param name="character">The updated character data.</param>
        /// <returns>NoContent if the update is successful, BadRequest if the ID in the URL does not match the character's ID.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterPutDTO character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }

            try
            {
                var characterToUpdate = _mapper.Map<Character>(character);
                var updatedCharacter = await _characterService.UpdateAsync(characterToUpdate);
                if (updatedCharacter == null) 
                {
                    var notFoundResponse = new NotFoundResponse($"Character with ID {id} not found.");
                    return NotFound(notFoundResponse);
                }
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new character.
        /// </summary>
        /// <param name="character">The character data to create.</param>
        /// <returns>The newly created character's data.</returns>
        [HttpPost]
        public async Task<ActionResult<CharacterDto>> PostCharacter(CharacterPostDTO character)
        {
            var characterToAdd = _mapper.Map<Character>(character);
            var addedCharacter = await _characterService.CreateAsync(characterToAdd);

            return CreatedAtAction("GetCharacter", 
                new { id = addedCharacter.Id },
                _mapper.Map<CharacterDto>(addedCharacter));
        }

        /// <summary>
        /// Deletes a character by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the character to delete.</param>
        /// <returns>NoContent if the deletion is successful, NotFound if the character with the specified ID is not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            try
            {
                var deletedCharacter = await _characterService.DeleteAsync(id);

                if (deletedCharacter == null)
                {
                    var notFoundResponse = new NotFoundResponse($"Character with ID {id} not found.");
                    return NotFound(notFoundResponse);
                }

                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                var notFoundResponse = new NotFoundResponse(ex.Message);
                return NotFound(notFoundResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
