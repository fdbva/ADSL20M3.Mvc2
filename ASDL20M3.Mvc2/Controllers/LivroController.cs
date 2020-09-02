using System.Linq;
using System.Threading.Tasks;
using ASDL20M3.Mvc2.HttpServices;
using Domain.Model.Models;
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
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = await _livroHttpClient.GetAllAsync();
            return View(bibliotecaContext.ToList());
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroHttpClient.GetByIdAsync(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }

            return View(livroModel);
        }

        // GET: Livro/Create
        public async Task<IActionResult> Create()
        {
            var autores = await _autorHttpClient.GetAllAsync();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorModel.Id), 
                nameof(AutorModel.NomeCompletoId));
            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            LivroModel livroModel)
        {
            if (ModelState.IsValid)
            {
                await _livroHttpClient.CreateAsync(livroModel);
                return RedirectToAction(nameof(Index));
            }
            var autores = await _autorHttpClient.GetAllAsync();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorModel.Id), 
                nameof(AutorModel.NomeCompletoId), 
                livroModel.AutorId);
            return View(livroModel);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroHttpClient.GetByIdAsync(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }
            var autores = await _autorHttpClient.GetAllAsync();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorModel.Id), 
                nameof(AutorModel.NomeCompletoId), 
                livroModel.AutorId);
            return View(livroModel);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            LivroModel livroModel)
        {
            if (id != livroModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _livroHttpClient.UpdateAsync(livroModel);
                return RedirectToAction(nameof(Index));
            }
            var autores = await _autorHttpClient.GetAllAsync();
            ViewData["AutorId"] = new SelectList(
                autores, 
                nameof(AutorModel.Id), 
                nameof(AutorModel.NomeCompletoId), 
                livroModel.AutorId);
            return View(livroModel);
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroHttpClient.GetByIdAsync(id.Value);
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
            await _livroHttpClient.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LivroModelExists(int id)
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
