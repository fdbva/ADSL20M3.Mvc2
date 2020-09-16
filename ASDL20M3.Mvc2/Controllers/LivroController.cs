using System.Linq;
using Application.AppServices;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASDL20M3.Mvc2.Controllers
{
    //[Authorize(Policy = "Admin")]
    public class LivroController : Controller
    {
        private readonly IAutorAppService _autorAppService;
        private readonly ILivroAppService _livroAppService;

        public LivroController(
            IAutorAppService autorAppService,
            ILivroAppService livroAppService)
        {
            _autorAppService = autorAppService;
            _livroAppService = livroAppService;
        }

        // GET: Livro
        public IActionResult Index(
            string searchText)
        {
            var livros = _livroAppService.GetAll(searchText);

            ViewBag.SearchText = searchText;

            return View(livros.ToList());
        }

        // GET: Livro/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroViewModel = _livroAppService.GetById(id.Value);
            if (livroViewModel == null)
            {
                return NotFound();
            }

            return View(livroViewModel);
        }

        // GET: Livro/Create
        public IActionResult Create()
        {
            var autores = _autorAppService.GetAll();
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

                _livroAppService.Create(livroAutorAggregateRequest);
                return RedirectToAction(nameof(Index));
            }
            var autores = _autorAppService.GetAll();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorViewModel.Id), 
                nameof(AutorViewModel.NomeCompletoId),
                livroAutorCreateViewModel.AutorId);
            return View(livroAutorCreateViewModel);
        }

        // GET: Livro/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroViewModel = _livroAppService.GetById(id.Value);
            if (livroViewModel == null)
            {
                return NotFound();
            }
            var autores = _autorAppService.GetAll();
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
        public IActionResult Edit(
            int id, 
            LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _livroAppService.Update(livroViewModel);
                return RedirectToAction(nameof(Index));
            }
            var autores = _autorAppService.GetAll();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorViewModel.Id), 
                nameof(AutorViewModel.NomeCompletoId), 
                livroViewModel.AutorId);
            return View(livroViewModel);
        }

        // GET: Livro/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroViewModel = _livroAppService.GetById(id.Value);
            if (livroViewModel == null)
            {
                return NotFound();
            }

            return View(livroViewModel);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _livroAppService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LivroViewModelExists(int id)
        {
            return _livroAppService.GetById(id) != null;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckIsbn(string isbn, int id)
        {
            if (!_livroAppService.CheckIsbn(isbn, id))
            {
                return Json($"ISBN {isbn} já está sendo usado.");
            }

            return Json(true);
        }
    }
}
