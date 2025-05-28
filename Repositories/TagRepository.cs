using Microsoft.EntityFrameworkCore;
using tag_news.Data;
using tag_news.Models;
using tag_news.Repositories.Interfaces;

namespace tag_news.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _context.Tags
                .Include(tag=>tag.NoticiaTags)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            var entity = _context.Add(tag);
            await _context.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            var entity = _context.Update(tag);
            await _context.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task<bool> DeleteAsync(Tag model)
        {
            try
            {
                _context.Tags.Remove(model);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
