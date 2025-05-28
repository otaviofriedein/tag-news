using AutoMapper;
using tag_news.Models;
using tag_news.Repositories.Interfaces;
using tag_news.Services.Intefaces;
using tag_news.Shared;
using tag_news.ViewModels;

namespace tag_news.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly INoticiaRepository _noticiaRepository;
        private readonly IMapper _mapper;

        public NoticiaService(INoticiaRepository noticiaRepository, IMapper mapper)
        {
            _noticiaRepository = noticiaRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<NoticiaViewModel>>> GetAllAsync()
        {
            var noticias = await _noticiaRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<NoticiaViewModel>>(noticias);

            return ServiceResult<IEnumerable<NoticiaViewModel>>.Ok(result);
        }

        public async Task<ServiceResult<NoticiaViewModel>> GetByIdAsync(int id)
        {
            var noticia = await _noticiaRepository.GetByIdAsync(id);

            if (noticia == null) return ServiceResult<NoticiaViewModel>.NaoEncontrado();

            var result = _mapper.Map<NoticiaViewModel>(noticia);

            return ServiceResult<NoticiaViewModel>.Ok(result);
        }

        public async Task<ServiceResult<NoticiaViewModel>> CreateAsync(NoticiaViewModel model)
        {
            var noticia = _mapper.Map<Noticia>(model);

            noticia.NoticiaTags = model.TagIds?.Select(tagId => new NoticiaTag { TagId = tagId }).ToList() ?? [];

            var noticiaDb = await _noticiaRepository.CreateAsync(noticia);

            var result = _mapper.Map<NoticiaViewModel>(noticiaDb);

            return ServiceResult<NoticiaViewModel>.Ok(result);
        }

        public async Task<ServiceResult<NoticiaViewModel>> UpdateAsync(int id, NoticiaViewModel model)
        {
            var noticiaDb = await _noticiaRepository.GetByIdAsync(id);

            if (noticiaDb == null) return ServiceResult<NoticiaViewModel>.NaoEncontrado();

            noticiaDb.Titulo = model.Titulo;
            noticiaDb.Texto = model.Texto;
            noticiaDb.UsuarioId = model.UsuarioId;

            // Atualizar tags
            noticiaDb.NoticiaTags.Clear();
            foreach (var tagId in model.TagIds ?? [])
            {
                noticiaDb.NoticiaTags.Add(new NoticiaTag { TagId = tagId });
            }

            var noticiaUpdated = await _noticiaRepository.UpdateAsync(noticiaDb);

            var result = _mapper.Map<NoticiaViewModel>(noticiaUpdated);
            return ServiceResult<NoticiaViewModel>.Ok(result);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var noticiaDb = await _noticiaRepository.GetByIdAsync(id);

            if (noticiaDb == null) return ServiceResult<bool>.NaoEncontrado();

            if (await _noticiaRepository.DeleteAsync(noticiaDb))
            {
                return ServiceResult<bool>.Ok(true);
            }
            else
            {
                return ServiceResult<bool>.Erro(["Ocorreu um erro ao excluir"]);
            }           
        }
             
        public async Task<ServiceResult<IEnumerable<TagViewModel>>> GetAllTagsAsync()
        {
            var tags = await _noticiaRepository.GetAllTagsAsync();

            var result = _mapper.Map<IEnumerable<TagViewModel>>(tags);

            return ServiceResult<IEnumerable<TagViewModel>>.Ok(result);
        }       
    }
} 