namespace Assignment3.Services
{
    public interface ICRUDService<T, TKey>
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);

    }
}
