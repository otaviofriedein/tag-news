using AutoMapper;
using tag_news.Models;
using tag_news.Repositories.Interfaces;
using tag_news.Services.Intefaces;
using tag_news.Shared;
using tag_news.ViewModels;

namespace tag_news.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<TagViewModel>>> GetAllAsync()
        {
            var tags = await _tagRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<TagViewModel>>(tags);

            return ServiceResult<IEnumerable<TagViewModel>>.Ok(result);
        }

        public async Task<ServiceResult<TagViewModel>> GetByIdAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null) return ServiceResult<TagViewModel>.NaoEncontrado();

            var result = _mapper.Map<TagViewModel>(tag);

            return ServiceResult<TagViewModel>.Ok(result);
        }

        public async Task<ServiceResult<TagViewModel>> CreateAsync(TagViewModel model)
        {
            var tag = _mapper.Map<Tag>(model);
            
            var tagDb = await _tagRepository.CreateAsync(tag);

            var result = _mapper.Map<TagViewModel>(tagDb);

            return ServiceResult<TagViewModel>.Ok(result);
        }

        public async Task<ServiceResult<TagViewModel>> UpdateAsync(int id, TagViewModel model)
        {
            var tagDb = await _tagRepository.GetByIdAsync(id);

            if (tagDb == null) return ServiceResult<TagViewModel>.NaoEncontrado();

            tagDb.Descricao = model.Descricao;           

            var tagUpdated = await _tagRepository.UpdateAsync(tagDb);

            var result = _mapper.Map<TagViewModel>(tagUpdated);
            return ServiceResult<TagViewModel>.Ok(result);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var tagDb = await _tagRepository.GetByIdAsync(id);

            if (tagDb == null) return ServiceResult<bool>.NaoEncontrado();

            if (tagDb.NoticiaTags.Count > 0) return ServiceResult<bool>.FalhaNegocial(["Existem noticias atreladas à Tag."]);

            if (await _tagRepository.DeleteAsync(tagDb))
            {
                return ServiceResult<bool>.Ok(true);
            }
            else
            {
                return ServiceResult<bool>.Erro(["Ocorreu um erro ao excluir"]);
            }
        }      
    }
} 