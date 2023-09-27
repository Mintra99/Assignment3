using Assignment3.Data.Dtos.Characters;
using Assignment3.Data.Dtos.Movies;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }


            return _mapper.Map<MovieDto>(movie);
        }

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
                return NotFound();
            }

            return NoContent();
        }


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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deletedMovie = await _movieService.DeleteAsync(id);


            if (deletedMovie == null)
            {
                return NotFound();
            }

            return Ok();
        }

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

    }
}