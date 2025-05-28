using Microsoft.AspNetCore.Mvc;
using tag_news.Services;
using tag_news.Services.Intefaces;
using tag_news.ViewModels;

namespace tag_news.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _tagService.GetAllAsync();

            return View(result.Dados);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TagViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao")] TagViewModel tag)
        {
            if (!ModelState.IsValid) return View(tag);

            var result = await _tagService.CreateAsync(tag);

            if (!result.Sucesso) return View(tag);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _tagService.GetByIdAsync(id);

            if (result.StatusCode == StatusCodes.Status404NotFound) return NotFound();

            return View(result.Dados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] TagViewModel tag)
        {            
            if (!ModelState.IsValid) return View(tag);

            var result = await _tagService.UpdateAsync(id, tag);

            if (result.StatusCode == StatusCodes.Status404NotFound) return NotFound();

            if (!result.Sucesso) return View(tag);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _tagService.DeleteAsync(id);

            if (result.StatusCode == StatusCodes.Status404NotFound) return NotFound();

            if (!result.Sucesso)
            {
                TempData["ErrorMessage"] = result.Mensagens;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}