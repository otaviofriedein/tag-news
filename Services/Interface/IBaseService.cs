namespace tag_news.Services
{
    public interface IBaseService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> CreateAsync(T model);
        Task<bool> UpdateAsync(T model);
        Task<(bool success, string message)> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
} 