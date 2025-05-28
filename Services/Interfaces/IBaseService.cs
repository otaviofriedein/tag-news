using tag_news.Shared;

namespace tag_news.Services.Intefaces
{
    public interface IBaseService<T>
    {
        Task<ServiceResult<IEnumerable<T>>> GetAllAsync();
        Task<ServiceResult<T>> GetByIdAsync(int id);
        Task<ServiceResult<T>> CreateAsync(T model);
        Task<ServiceResult<T>> UpdateAsync(int id, T model);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}

