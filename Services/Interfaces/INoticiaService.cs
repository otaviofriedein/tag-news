using tag_news.Shared;
using tag_news.ViewModels;

namespace tag_news.Services.Intefaces
{
    public interface INoticiaService : IBaseService<NoticiaViewModel>
    {
        Task<ServiceResult<IEnumerable<TagViewModel>>> GetAllTagsAsync();
    }
} 