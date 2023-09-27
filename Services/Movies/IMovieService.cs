using Assignment3.Models;

namespace Assignment3.Services.Movies
{
    public interface IMovieService : ICRUDService<Movie, int>
    {
        Task<ICollection<Character>> GetCharactersAsync(int id);
    }
}
