using Assignment3.Models;

namespace Assignment3.Services.Movies
{
    public interface IMovieService : ICRUDService<Movie, int>
    {
        /// <summary>
        /// Retrieves a list of characters associated with the specified movie
        /// </summary>
        /// <param name="id">The id of the movie</param>
        /// <returns>A list of <see cref="Character"/> associated with the movie</returns>
        Task<ICollection<Character>> GetCharactersAsync(int id);
        Task UpdateCharactersAsync(int id, int[] characterIds);
    }
}
