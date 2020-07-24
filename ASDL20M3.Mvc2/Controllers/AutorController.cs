using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASDL20M3.Mvc2.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;

        //Lembrar de registrar dependência no Startup.cs
        public AutorController(
            IAutorService autorService)
        {
            _autorService = autorService;
        }

        // GET: Autor
        public IActionResult Index()
        {
            var todosAutores = _autorService.GetAll();
            return View(todosAutores);
        }

        // GET: Autor/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = _autorService.GetById(id.Value);
            if (autorModel == null)
            {
                return NotFound();
            }

            return View(autorModel);
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,UltimoNome,Nascimento")] AutorModel autorModel)
        {
            if (ModelState.IsValid)
            {
                _autorService.Create(autorModel);
                return RedirectToAction(nameof(Index));
            }
            return View(autorModel);
        }

        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = _autorService.GetById(id.Value);
            if (autorModel == null)
            {
                return NotFound();
            }
            return View(autorModel);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,UltimoNome,Nascimento")] AutorModel autorModel)
        {
            if (id != autorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _autorService.Update(autorModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorModelExists(autorModel.Id))
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
            return View(autorModel);
        }

        // GET: Autor/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = _autorService.GetById(id.Value);
            if (autorModel == null)
            {
                return NotFound();
            }

            return View(autorModel);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _autorService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AutorModelExists(int id)
        {
            var autorEncontrado = _autorService.GetById(id);
            return autorEncontrado != null;
        }
    }
}
