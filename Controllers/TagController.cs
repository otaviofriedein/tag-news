using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tag_news.Data;
using tag_news.Models;

namespace tag_news.Controllers
{
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tag
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tags.ToListAsync());
        }

        // GET: Tag/Create
        public IActionResult Create()
        {
            return View(new Tag());
        }

        // POST: Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }

        // GET: Tag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();            

            var tag = await _context.Tags.FindAsync(id);

            if (tag == null) return NotFound();            

            return View(tag);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] Tag tag)
        {
            if (id != tag.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags
                .Include(t => t.NoticiaTags)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag == null) return NotFound();
            
            // Verifica se existem notícias associadas à tag
            if (tag.NoticiaTags.Any())
            {
                TempData["ErrorMessage"] = "Não é possível excluir esta tag pois existem notícias associadas a ela.";
                return RedirectToAction(nameof(Index));
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}