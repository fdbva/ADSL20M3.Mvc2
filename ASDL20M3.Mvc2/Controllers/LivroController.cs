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

        public override IActionResult Create()
        {
            var autores = _autorCrudAppService.GetAll();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorViewModel.Id), 
                nameof(AutorViewModel.NomeCompletoId),
                null);
            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            LivroAutorCreateViewModel livroAutorCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var livroAutorAggregateRequest = 
                    new LivroAutorAggregateRequest(livroAutorCreateViewModel);

                _livroCrudAppService.Create(livroAutorAggregateRequest);
                return RedirectToAction(nameof(Index));
            }
            var autores = _autorCrudAppService.GetAll();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorViewModel.Id), 
                nameof(AutorViewModel.NomeCompletoId),
                livroAutorCreateViewModel.AutorId);
            return View(livroAutorCreateViewModel);
        }

        // GET: Livro/Edit/5
        public override IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroViewModel = _livroCrudAppService.GetById(id.Value);
            if (livroViewModel == null)
            {
                return NotFound();
            }
            var autores = _autorCrudAppService.GetAll();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorViewModel.Id), 
                nameof(AutorViewModel.NomeCompletoId), 
                livroViewModel.AutorId);
            return View(livroViewModel);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override IActionResult Edit(
            int id, 
            LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _livroCrudAppService.Update(livroViewModel);
                return RedirectToAction(nameof(Index));
            }
            var autores = _autorCrudAppService.GetAll();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorViewModel.Id), 
                nameof(AutorViewModel.NomeCompletoId), 
                livroViewModel.AutorId);
            return View(livroViewModel);
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
