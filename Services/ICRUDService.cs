using Assignment3.Helpers.Exceptions;

namespace Assignment3.Services
{
    public interface ICRUDService<T, TKey>
    {
        /// <summary>
        /// Creates an instances of <typeparamref name="T"/> in the database.
        /// </summary>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Retrieves a list of all <typeparamref name="T"/> from the database
        /// </summary>
        /// <returns>A list of <typeparamref name="T"/></returns>
        Task<List<T>> GetAsync();

        /// <summary>
        /// Retrieves a <typeparamref name="T"/> by its Id
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <returns>The <typeparamref name="T"/> with the specified id</returns>
        /// <exception cref="EntityNotFoundException">Thrown if the specified entity with <paramref name="id"/> is not found in the database.</exception>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Updates an existing <typeparamref name="T"/> in the database.
        /// </summary>
        /// <param name="entity">The updated entity to save in the database.</param>
        /// <returns>The updated <typeparamref name="T"/></returns>
        /// <exception cref="EntityNotFoundException">Thrown if the specified entity to update is not found in the database.</exception>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Deletes a <typeparamref name="T"/> from the database by its id.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        /// <returns>The deleted <typeparamref name="T"/></returns>
        /// <exception cref="EntityNotFoundException">Thrown if the specified entity with <paramref name="id"/> is not found in the database.</exception>
        Task<T> DeleteAsync(int id);

    }
}
