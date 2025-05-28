using Microsoft.AspNetCore.Mvc;
using tag_news.Services.Intefaces;
using tag_news.Shared;
using tag_news.ViewModels;

namespace tag_news.Controllers
{
    public class NoticiaController : Controller
    {
        private readonly INoticiaService _noticiaService;

        public NoticiaController(INoticiaService noticiaService)
        {
            _noticiaService = noticiaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _noticiaService.GetAllAsync();

            return View(result.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var resultTags = await _noticiaService.GetAllTagsAsync();

            ViewBag.Tags = resultTags.Dados;
            return View(new NoticiaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoticiaViewModel model)
        {
            if (!ModelState.IsValid) return RetornarErrosModelState();            

            var result = await _noticiaService.CreateAsync(model);

            if (!result.Sucesso) return Json(ServiceResult<NoticiaViewModel>.FalhaNegocial(result.Mensagem));

            return Json(ServiceResult<NoticiaViewModel>.Ok(result.Dados));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {            
            var result = await _noticiaService.GetByIdAsync(id);

            if (result.StatusCode == StatusCodes.Status404NotFound) return NotFound();

            var resultTags = await _noticiaService.GetAllTagsAsync();
            ViewBag.Tags = resultTags.Dados;

            return View(result.Dados);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] NoticiaViewModel model)
        {
            if (!ModelState.IsValid) return RetornarErrosModelState();            

            var result = await _noticiaService.UpdateAsync(id, model);

            if (result.StatusCode == StatusCodes.Status404NotFound) 
                return Json(ServiceResult<NoticiaViewModel>.NaoEncontrado());

            if (!result.Sucesso) return Json(ServiceResult<NoticiaViewModel>.FalhaNegocial(result.Mensagem));

            return Json(ServiceResult<NoticiaViewModel>.Ok(result.Dados));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _noticiaService.DeleteAsync(id);

            if (result.StatusCode == StatusCodes.Status404NotFound) return NotFound();
                 
            if (!result.Sucesso) return BadRequest();

            return RedirectToAction(nameof(Index));

        }

        private JsonResult RetornarErrosModelState()
        {
            var erros = string.Join(Environment.NewLine, ModelState.Values
                                                               .SelectMany(v => v.Errors)
                                                               .Select(e => e.ErrorMessage)
             );

            return Json(ServiceResult<NoticiaViewModel>.FalhaNegocial(erros));
        }
    }
}
