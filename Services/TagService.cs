using Microsoft.EntityFrameworkCore;
using tag_news.Data;
using tag_news.Models;

namespace tag_news.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Tag tag)
        {
            try
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Tag tag)
        {
            try
            {
                _context.Update(tag);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(tag.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<(bool success, string message)> DeleteAsync(int id)
        {
            var tag = await _context.Tags
                .Include(t => t.NoticiaTags)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag == null)
            {
                return (false, "Tag não encontrada.");
            }

            if (tag.NoticiaTags.Any())
            {
                return (false, "Não é possível excluir esta tag pois existem notícias associadas a ela.");
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return (true, "Tag excluída com sucesso.");
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Tags.AnyAsync(e => e.Id == id);
        }
    }
} 