using Assignment3.Models;
using Microsoft.Data.SqlClient;

namespace Assignment3.Services.Franchises
{
    public interface IFranchiseService : ICRUDService<Franchise, int>
    {
        /// <summary>
        /// Retrieves a list of movies associated with the specified franchise
        /// </summary>
        /// <param name="id">The id of the franchise</param>
        /// <returns>A list of <see cref="Movie"/> associated with the franchise</returns>
        Task<List<Movie>> GetMoviesAsync(int id);

        /// <summary>
        /// Updates the list of movies associated with the specified franchise
        /// </summary>
        /// <param name="id">The id of the franchise</param>
        /// <param name="movies">An array of id representing movies to associate with the franchise</param>
        Task UpdateMoviesAsync(int id, int[] movies);

        /// <summary>
        /// Retrieves a list of characters associated with the specified franchise
        /// </summary>
        /// <param name="id">The id of the franchise</param>
        /// <returns>A list of <see cref="Character"/> associated with the franchise</returns>
        Task<List<Character>> GetCharactersAsync(int id);

    }
}
