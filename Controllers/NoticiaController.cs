using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tag_news.Data;
using tag_news.Models;
using tag_news.ViewModels;

namespace Noticias.Controllers
{
    public class NoticiaController : Controller
    {
        private readonly AppDbContext _context;

        public NoticiaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Noticia
        public async Task<IActionResult> Index()
        {
            var noticias = await _context.Noticias
                .Include(n => n.Usuario)
                .Include(n => n.NoticiaTags)
                    .ThenInclude(nt => nt.Tag)
                .ToListAsync();

            return View(noticias);
        }

        // GET: Noticia/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Tags = await _context.Tags.ToListAsync();
            
            return View(new NoticiaViewModel());
        }

        // POST: Noticia/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoticiaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var noticia = new Noticia
                {
                    Titulo = model.Titulo,
                    Texto = model.Texto,
                    UsuarioId = model.UsuarioId, // TODO: Nao implementado
                    NoticiaTags = model.TagIds?.Select(tagId => new NoticiaTag { TagId = tagId }).ToList() ?? []
                };

                _context.Add(noticia);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        // GET: Noticia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();            

            var noticia = await _context.Noticias
                .Include(n => n.NoticiaTags)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (noticia == null) return NotFound();            

            var viewModel = new NoticiaViewModel
            {
                Id = noticia.Id,
                Titulo = noticia.Titulo,
                Texto = noticia.Texto,
                UsuarioId = noticia.UsuarioId, // TODO: Nao implementado
                TagIds = noticia.NoticiaTags.Select(nt => nt.TagId).ToList()
            };

            ViewBag.Tags = await _context.Tags.ToListAsync();
            return View(viewModel);
        }

        // POST: Noticia/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] NoticiaViewModel model)
        {
            if (id != model.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    var noticia = await _context.Noticias
                        .Include(n => n.NoticiaTags)
                        .FirstOrDefaultAsync(n => n.Id == id);

                    if (noticia == null) return NotFound();

                    noticia.Titulo = model.Titulo;
                    noticia.Texto = model.Texto;
                    noticia.UsuarioId = model.UsuarioId; // TODO: Nao implementado

                    // Atualizar tags
                    noticia.NoticiaTags.Clear();
                    foreach (var tagId in model.TagIds ?? new List<int>())
                    {
                        noticia.NoticiaTags.Add(new NoticiaTag { TagId = tagId });
                    }

                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticiaExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        // POST: Noticia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noticia = await _context.Noticias.FindAsync(id);

            if (noticia != null)
            {
                _context.Noticias.Remove(noticia);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticias.Any(e => e.Id == id);
        }
    }
}
