using System;
using System.Linq;
using System.Threading.Tasks;
using ASDL20M3.Mvc2.HttpServices;
using ASDL20M3.Mvc2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASDL20M3.Mvc2.Controllers
{
    //[Authorize(Policy = "Admin")]
    public class LivroController : Controller
    {
        private readonly IAutorHttpClient _autorHttpClient;
        private readonly ILivroHttpClient _livroHttpClient;

        public LivroController(
            IAutorHttpClient autorHttpClient,
            ILivroHttpClient livroHttpClient)
        {
            _autorHttpClient = autorHttpClient;
            _livroHttpClient = livroHttpClient;
        }

        // GET: Livro
        public async Task<IActionResult> Index(
            string searchText)
        {
            var livros = await _livroHttpClient.GetAllAsync(searchText);

            ViewBag.SearchText = searchText;

            return View(livros.ToList());
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroViewModel = await _livroHttpClient.GetByIdAsync(id.Value);
            if (livroViewModel == null)
            {
                return NotFound();
            }

            return View(livroViewModel);
        }

        // GET: Livro/Create
        public async Task<IActionResult> Create()
        {
            var autores = await _autorHttpClient.GetAllAsync();
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
        public async Task<IActionResult> Create(
            LivroAutorCreateViewModel livroAutorCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var livroAutorAggregateViewModel = 
                    new LivroAutorAggregateRequest(livroAutorCreateViewModel);

                await _livroHttpClient.CreateAsync(livroAutorAggregateViewModel);
                return RedirectToAction(nameof(Index));
            }
            var autores = await _autorHttpClient.GetAllAsync();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorViewModel.Id), 
                nameof(AutorViewModel.NomeCompletoId),
                livroAutorCreateViewModel.AutorId);
            return View(livroAutorCreateViewModel);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroViewModel = await _livroHttpClient.GetByIdAsync(id.Value);
            if (livroViewModel == null)
            {
                return NotFound();
            }
            var autores = await _autorHttpClient.GetAllAsync();
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
        public async Task<IActionResult> Edit(
            int id, 
            LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _livroHttpClient.UpdateAsync(livroViewModel);
                return RedirectToAction(nameof(Index));
            }
            var autores = await _autorHttpClient.GetAllAsync();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorViewModel.Id), 
                nameof(AutorViewModel.NomeCompletoId), 
                livroViewModel.AutorId);
            return View(livroViewModel);
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroViewModel = await _livroHttpClient.GetByIdAsync(id.Value);
            if (livroViewModel == null)
            {
                return NotFound();
            }

            return View(livroViewModel);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _livroHttpClient.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LivroViewModelExists(int id)
        {
            return await _livroHttpClient.GetByIdAsync(id) != null;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckIsbn(string isbn, int id)
        {
            if (!await _livroHttpClient.CheckIsbn(isbn, id))
            {
                return Json($"ISBN {isbn} já está sendo usado.");
            }

            return Json(true);
        }
    }
}
