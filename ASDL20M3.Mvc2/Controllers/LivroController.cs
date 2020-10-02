using System.Linq;
using Application.AppServices;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASDL20M3.Mvc2.Controllers
{
    //[Authorize(Policy = "Admin")]
    public class LivroController : BaseCrudController<LivroViewModel>
    {
        private readonly IAutorCrudAppService _autorCrudAppService;
        private readonly ILivroCrudAppService _livroCrudAppService;

        public LivroController(
            IAutorCrudAppService autorCrudAppService,
            ILivroCrudAppService livroCrudAppService) : base(livroCrudAppService)
        {
            _autorCrudAppService = autorCrudAppService;
            _livroCrudAppService = livroCrudAppService;
        }

        protected override void PrepareViewData(int? id = null)
        {
            var autores = _autorCrudAppService.GetAll();
            ViewData["AutorId"] = new SelectList(
                autores,
                nameof(AutorViewModel.Id),
                nameof(AutorViewModel.NomeCompletoId),
                id);
        }

        protected override void PrepareViewData(LivroViewModel livroViewModel)
        {
            PrepareViewData(livroViewModel.AutorId);
        }

        // POST: Livro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAggregate(
            LivroAutorCreateViewModel livroAutorCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var livroAutorAggregateRequest = 
                    new LivroAutorAggregateRequest(livroAutorCreateViewModel);

                _livroCrudAppService.Create(livroAutorAggregateRequest);
                return RedirectToAction(nameof(Index));
            }

            PrepareViewData(livroAutorCreateViewModel.AutorId);
            return View("Create", livroAutorCreateViewModel);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckIsbn(string isbn, int id)
        {
            if (!_livroCrudAppService.CheckIsbn(isbn, id))
            {
                return Json($"ISBN {isbn} já está sendo usado.");
            }

            return Json(true);
        }
    }
}
