namespace Assignment3.Services
{
    public interface ICRUDService<T>
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> GetAsync();
        Task<T> GetById(int id);
        Task<T> Update(string name);

    }
}
