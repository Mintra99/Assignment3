using Assignment3.Data.Dtos.Characters;
using Assignment3.Data.Dtos.Movies;
using Assignment3.Helpers;
using Assignment3.Helpers.Exceptions;
using Assignment3.Models;
using Assignment3.Services.Movies;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Controllers
{
    [ApiController]
    [Route("api/v1/Movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;


        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all movies
        /// </summary>
        /// <returns>All movies</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _movieService.GetAsync();

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(movies));
        }


        /// <summary>
        /// Retrieves a single movie by its id
        /// </summary>
        /// <param name="id">The id of the movie to retrieve.</param>
        /// <returns>
        /// If found, returns the specified Movie. 
        /// If not found, returns a NotFound response with an error message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound(new NotFoundResponse($"Movie with Id: {id} could not be found."));
            }


            return _mapper.Map<MovieDto>(movie);
        }

        /// <summary>
        /// Updates an existing movie with the specified id
        /// </summary>
        /// <param name="id">The id of the movie to update.</param>
        /// <param name="movie">The updated movie</param>
        /// <returns>
        /// If the update is successful, returns a 204 No Content response.
        /// If the movie with the given id is not found, returns a 404 NotFound response.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MoviePutDto movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            var movieToUpdate = _mapper.Map<Movie>(movie);
            var updatedMovie = await _movieService.UpdateAsync(movieToUpdate);

            if (updatedMovie == null)
            {
                return NotFound(new NotFoundResponse($"Movie with Id: {id} could not be found."));
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new movie
        /// </summary>
        /// <param name="movie">The movie data to create</param>
        /// <returns>
        /// If the creation is successful, returns a 201 Created response with the newly created movie's details.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MoviePostDto movie)
        {
            var movieToAdd = _mapper.Map<Movie>(movie);
            var addedFrannchise = await _movieService.CreateAsync(movieToAdd);


            return CreatedAtAction(
                nameof(GetMovie),
                new { id = addedFrannchise.Id },
                _mapper.Map<MovieDto>(addedFrannchise));
        }

        /// <summary>
        /// Deletes a movie by its id
        /// </summary>
        /// <param name="id">The id of the movie to delete</param>
        /// <returns>
        /// If the deletion is successful, returns a 200 OK response.
        /// If the movie with the given id is not found, returns a 404 NotFound response.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deletedMovie = await _movieService.DeleteAsync(id);


            if (deletedMovie == null)
            {
                return NotFound(new NotFoundResponse($"Movie with Id: {id} could not be found."));
            }

            return Ok();
        }

        /// <summary>
        /// Retrieves all characters associated with a movie by its id.
        /// </summary>
        /// <param name="id">The id of the movie to retrieve characters for.</param>
        /// <returns>
        /// If the movie is found, returns a list of character details associated with the movie.
        /// If the movie with the given id is not found, returns a 404 NotFound response.
        /// </returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAllCharacters(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CharacterDto>>(await _movieService.GetCharactersAsync(id)));
        }

        /// <summary>
        /// Updates the list of characters associated with a movie.
        /// </summary>
        /// <param name="id">The ID of the movie to update characters for.</param>
        /// <param name="characterIds">An array of current + new character IDs to associate with the movie.</param>
        /// <returns>Returns NoContent if the update is successful.</returns>
        [HttpPut("{id}/characters")]
        public async Task<ActionResult> PutMovieCharacters(int id, [FromBody] int[] characterIds)
        {
            try
            {
                await _movieService.UpdateCharactersAsync(id, characterIds);
                return NoContent();
            }
            catch (EntityNotFoundException err)
            {
                return NotFound(new NotFoundResponse(err.Message));
            }
        }

    }
}