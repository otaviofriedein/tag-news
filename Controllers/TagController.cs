using Microsoft.AspNetCore.Mvc;
using tag_news.Models;
using tag_news.Services;

namespace tag_news.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: Tag
        public async Task<IActionResult> Index()
        {
            return View(await _tagService.GetAllAsync());
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
            if (!ModelState.IsValid)
            {
                return View(tag);
            }

            if (await _tagService.CreateAsync(tag))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }

        // GET: Tag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tag = await _tagService.GetByIdAsync(id.Value);
            if (tag == null) return NotFound();

            return View(tag);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] Tag tag)
        {
            if (id != tag.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(tag);
            }

            if (await _tagService.UpdateAsync(tag))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (success, message) = await _tagService.DeleteAsync(id);

            if (!success)
            {
                TempData["ErrorMessage"] = message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}