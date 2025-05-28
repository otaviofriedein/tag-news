using tag_news.Models;

namespace tag_news.Services
{
    public interface INoticiaService : IBaseService<Noticia>
    {
        Task<(bool success, string message)> CreateAsync(NoticiaViewModel model);
        Task<(bool success, string message)> UpdateAsync(int id, NoticiaViewModel model);      
        Task<IEnumerable<Tag>> GetAllTagsAsync();
    }
} 