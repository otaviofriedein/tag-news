using tag_news.Models;

namespace tag_news.Repositories.Interfaces
{
    public interface INoticiaRepository : IBaseRepository<Noticia>
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
    }
}
