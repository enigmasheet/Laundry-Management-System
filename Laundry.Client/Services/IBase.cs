

namespace Laundry.Client.Services
{
    /// <summary>
    /// Base CRUD interface for client-side data operations.
    /// </summary>
    /// <typeparam name="TDto">The type of DTO being managed.</typeparam>
    /// <typeparam name="TKey">The type of the primary key (e.g., int, Guid).</typeparam>
    public interface IBase<TDto, TKey>
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(TKey id);
        Task<TDto?> GetAllByIDAsync(TKey id);
        Task<TDto> CreateAsync(TDto dto);
        Task UpdateAsync(TKey id, TDto dto);
        Task DeleteAsync(TKey id);
    }
}
