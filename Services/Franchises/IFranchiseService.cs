using Assignment3.Models;
using Microsoft.Data.SqlClient;

namespace Assignment3.Services.Franchises
{
    public interface IFranchiseService : ICRUDService<Franchise, int>
    {
        Task<List<Movie>> GetMoviesAsync(int id);
        Task UpdateMoviesAsync(int id, int[] movies);
    }
}
