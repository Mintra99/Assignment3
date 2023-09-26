using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Models;
using Assignment3.Services.Franchises;
using AutoMapper;
using Assignment3.Data.Dtos.Franchises;
using Assignment3.Data.Dtos.Movies;
using Assignment3.Exceptionhandler;
using Assignment3.Data.Dtos.Characters;
using Assignment3.Helpers;

namespace Assignment3.Controllers
{
    [Route("api/v1/Franchise")]
    [ApiController]
    [Produces("application/Json")]
    [Consumes("application/Json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;

        public FranchiseController(IFranchiseService franchiseService, IMapper mapper)
        {
            _franchiseService = franchiseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all franchises
        /// </summary>
        /// <returns>All franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetFranchises()
        {
            var franchises = await _franchiseService.GetAsync();

            return Ok(_mapper.Map<IEnumerable<FranchiseDto>>(franchises));
        }

        /// <summary>
        /// Gets a specific franchise by its ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to retrieve.</param>
        /// <returns>Returns the franchise with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDto>> GetFranchise(int id)
        {
            try
            {
                var franchise = await _franchiseService.GetByIdAsync(id);
                return _mapper.Map<FranchiseDto>(franchise);
            }
            catch (Exception err)
            {
                return NotFound(new NotFoundResponse(err.Message));
            }

            
        }

        /// <summary>
        /// Updates an existing franchise.
        /// </summary>
        /// <param name="id">The ID of the franchise to update.</param>
        /// <param name="franchise">The updated franchise data.</param>
        /// <returns>Returns NoContent if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchisePutDto franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            var franchiseToUpdate = _mapper.Map<Franchise>(franchise);

            try
            {
                var updatedFranchise = await _franchiseService.UpdateAsync(franchiseToUpdate);
                return NoContent();

            }
            catch (EntityNotFoundException err)
            {
                return NotFound(new NotFoundResponse(err.Message));
            }

        }

        /// <summary>
        /// Creates a new franchise.
        /// </summary>
        /// <param name="franchise">The franchise data to create.</param>
        /// <returns>Returns the newly created franchise.</returns>
        [HttpPost]
        public async Task<ActionResult<FranchiseDto>> PostFranchise(FranchisePostDto franchise)
        {
            var franchiseToAdd = _mapper.Map<Franchise>(franchise);
            var addedFrannchise = await _franchiseService.CreateAsync(franchiseToAdd);


            return CreatedAtAction(
                nameof(GetFranchise),
                new { id = addedFrannchise.Id},
                _mapper.Map<FranchiseDto>(addedFrannchise));
        }

        /// <summary>
        /// Deletes a franchise by its ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to delete.</param>
        /// <returns>Returns Ok if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            try
            {
                var deletedFranchise = await _franchiseService.DeleteAsync(id);

                return Ok();
            }
            catch (EntityNotFoundException err)
            {
                return NotFound(new NotFoundResponse(err.Message));
            }

        }

        /// <summary>
        /// Gets all movies associated with a specific franchise.
        /// </summary>
        /// <param name="id">The ID of the franchise to retrieve movies for.</param>
        /// <returns>Returns a list of movies associated with the franchise.</returns>
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies(int id)
        {
            try
            {
                var movies = await _franchiseService.GetMoviesAsync(id);
                return Ok(_mapper.Map<IEnumerable<MovieDto>>(movies));
            }
            catch (EntityNotFoundException err)
            {
                return NotFound(new NotFoundResponse(err.Message));
            }
        }

        /// <summary>
        /// Updates the list of movies associated with a franchise.
        /// </summary>
        /// <param name="id">The ID of the franchise to update movies for.</param>
        /// <param name="movieIds">An array of movie IDs to associate with the franchise.</param>
        /// <returns>Returns NoContent if the update is successful.</returns>
        [HttpPut("{id}/movies")]
        public async Task<ActionResult> PutFranchiseMovies(int id, [FromBody] int[] movieIds)
        {
            try
            {
                await _franchiseService.UpdateMoviesAsync(id, movieIds);
                return NoContent();
            }
            catch (EntityNotFoundException err)
            {
                return NotFound(new NotFoundResponse(err.Message));
            }
        }

        /// <summary>
        /// Gets all characters associated with a specific franchise.
        /// </summary>
        /// <param name="id">The ID of the franchise to retrieve characters for.</param>
        /// <returns>Returns a list of characters associated with the franchise.</returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetFranchiseCharacters(int id)
        {
            try
            {
                var characters = await _franchiseService.GetCharactersAsync(id);
                var charDto = _mapper.Map<IEnumerable<CharacterDto>>(characters);
                return Ok(charDto);
            }
            catch (EntityNotFoundException err)
            {
                return NotFound(new NotFoundResponse(err.Message));
            }
        }
    }
}
