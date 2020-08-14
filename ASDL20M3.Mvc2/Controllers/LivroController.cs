using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASDL20M3.Mvc2.Controllers
{
    [Authorize(Policy = "Admin")]
    public class LivroController : Controller
    {
        private readonly IAutorService _autorService;
        private readonly ILivroService _livroService;

        public LivroController(
            IAutorService autorService,
            ILivroService livroService)
        {
            _autorService = autorService;
            _livroService = livroService;
        }

        // GET: Livro
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _livroService.GetAll();
            return View(bibliotecaContext.ToList());
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = _livroService.GetById(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }

            return View(livroModel);
        }

        // GET: Livro/Create
        public IActionResult Create()
        {
            var autores = _autorService.GetAll();
            ViewData["AutorId"] = new SelectList(autores, "Id", "Id");
            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Isbn,Titulo,Paginas,Lancamento,AutorId")] LivroModel livroModel)
        {
            if (ModelState.IsValid)
            {
                _livroService.Create(livroModel);
                return RedirectToAction(nameof(Index));
            }
            var autores = _autorService.GetAll();
            ViewData["AutorId"] = new SelectList(autores, "Id", "Id", livroModel.AutorId);
            return View(livroModel);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = _livroService.GetById(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }
            var autores = _autorService.GetAll();
            ViewData["AutorId"] = new SelectList(autores, "Id", "Id", livroModel.AutorId);
            return View(livroModel);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Isbn,Titulo,Paginas,Lancamento,AutorId")] LivroModel livroModel)
        {
            if (id != livroModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _livroService.Update(livroModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroModelExists(livroModel.Id))
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
            var autores = _autorService.GetAll();
            ViewData["AutorId"] = new SelectList(autores, "Id", "Id", livroModel.AutorId);
            return View(livroModel);
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = _livroService.GetById(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }

            return View(livroModel);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _livroService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LivroModelExists(int id)
        {
            return _livroService.GetById(id) != null;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckIsbn(string isbn, int id)
        {
            if (!_livroService.CheckIsbn(isbn, id))
            {
                return Json($"ISBN {isbn} já está sendo usado.");
            }

            return Json(true);
        }
    }
}
