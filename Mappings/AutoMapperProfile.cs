using AutoMapper;
using tag_news.Models;
using tag_news.ViewModels;

namespace tag_news.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Noticia, NoticiaViewModel>().ReverseMap();
            CreateMap<Tag, TagViewModel>().ReverseMap();
        }
    }
}
