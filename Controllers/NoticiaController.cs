using Microsoft.AspNetCore.Mvc;
using tag_news.Services;

namespace tag_news.Controllers
{
    public class NoticiaController : Controller
    {
        private readonly INoticiaService _noticiaService;

        public NoticiaController(INoticiaService noticiaService)
        {
            _noticiaService = noticiaService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _noticiaService.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Tags = await _noticiaService.GetAllTagsAsync();
            return View(new NoticiaViewModel { TagIds = new List<int>() });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoticiaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            var (success, message) = await _noticiaService.CreateAsync(model);
            return Json(new { success, message });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var noticia = await _noticiaService.GetByIdAsync(id.Value);
            if (noticia == null) return NotFound();

            var viewModel = new NoticiaViewModel
            {
                Id = noticia.Id,
                Titulo = noticia.Titulo,
                Texto = noticia.Texto,
                UsuarioId = noticia.UsuarioId,
                TagIds = noticia.NoticiaTags.Select(nt => nt.TagId).ToList()
            };

            ViewBag.Tags = await _noticiaService.GetAllTagsAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] NoticiaViewModel model)
        {
            if (id != model.Id)
            {
                return Json(new { success = false, message = "ID inválido." });
            }

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            var (success, message) = await _noticiaService.UpdateAsync(id, model);
            return Json(new { success, message });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (success, message) = await _noticiaService.DeleteAsync(id);
            
            if (!success)
            {
                TempData["ErrorMessage"] = message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
