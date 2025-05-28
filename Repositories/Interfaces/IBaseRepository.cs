namespace tag_news.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> CreateAsync(T model);
        Task<T?> UpdateAsync(T model);
        Task<bool> DeleteAsync(T model);
    }
}
