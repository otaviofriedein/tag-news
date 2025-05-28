using Microsoft.EntityFrameworkCore;
using tag_news.Data;
using tag_news.Models;

namespace tag_news.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly AppDbContext _context;

        public NoticiaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Noticia>> GetAllAsync()
        {
            return await _context.Noticias
                .Include(n => n.Usuario)
                .Include(n => n.NoticiaTags)
                    .ThenInclude(nt => nt.Tag)
                .ToListAsync();
        }

        public async Task<Noticia?> GetByIdAsync(int id)
        {
            return await _context.Noticias
                .Include(n => n.NoticiaTags)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<(bool success, string message)> CreateAsync(NoticiaViewModel model)
        {
            try
            {
                var noticia = new Noticia
                {
                    Titulo = model.Titulo,
                    Texto = model.Texto,
                    UsuarioId = model.UsuarioId,
                    NoticiaTags = model.TagIds?.Select(tagId => new NoticiaTag { TagId = tagId }).ToList() ?? new List<NoticiaTag>()
                };

                _context.Add(noticia);
                await _context.SaveChangesAsync();
                return (true, "Notícia criada com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar notícia: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> UpdateAsync(int id, NoticiaViewModel model)
        {
            try
            {
                var noticia = await _context.Noticias
                    .Include(n => n.NoticiaTags)
                    .FirstOrDefaultAsync(n => n.Id == id);

                if (noticia == null)
                {
                    return (false, "Notícia não encontrada.");
                }

                noticia.Titulo = model.Titulo;
                noticia.Texto = model.Texto;
                noticia.UsuarioId = model.UsuarioId;

                // Atualizar tags
                noticia.NoticiaTags.Clear();
                foreach (var tagId in model.TagIds ?? new List<int>())
                {
                    noticia.NoticiaTags.Add(new NoticiaTag { TagId = tagId });
                }

                await _context.SaveChangesAsync();
                return (true, "Notícia atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar notícia: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> DeleteAsync(int id)
        {
            try
            {
                var noticia = await _context.Noticias.FindAsync(id);
                if (noticia == null)
                {
                    return (false, "Notícia não encontrada.");
                }

                _context.Noticias.Remove(noticia);
                await _context.SaveChangesAsync();
                return (true, "Notícia excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao excluir notícia: {ex.Message}");
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Noticias.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public Task<bool> CreateAsync(Noticia model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Noticia model)
        {
            throw new NotImplementedException();
        }
    }
} 