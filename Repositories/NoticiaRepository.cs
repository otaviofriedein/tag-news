using Microsoft.EntityFrameworkCore;
using tag_news.Data;
using tag_news.Models;
using tag_news.Repositories.Interfaces;

namespace tag_news.Repositories
{
    public class NoticiaRepository : INoticiaRepository
    {
        private readonly AppDbContext _context;

        public NoticiaRepository(AppDbContext context)
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

        public async Task<Noticia> CreateAsync(Noticia noticia)
        {
            var entity = _context.Add(noticia);
            await _context.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task<Noticia> UpdateAsync(Noticia noticia)
        {
            var entity = _context.Update(noticia);
            await _context.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task<bool> DeleteAsync(Noticia noticia)
        {
            try
            {
                _context.Noticias.Remove(noticia);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }            
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

    }
}
