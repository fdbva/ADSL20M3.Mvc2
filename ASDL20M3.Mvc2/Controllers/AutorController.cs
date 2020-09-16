using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.AppServices;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASDL20M3.Mvc2.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorAppService _autorAppService;

        //Lembrar de registrar dependência no Startup.cs
        public AutorController(
            IAutorAppService autorAppService)
        {
            _autorAppService = autorAppService;
        }

        // GET: Autor
        public IActionResult Index()
        {
            var autores = _autorAppService.GetAll();

            return View(autores);
        }

        // GET: Autor/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var autorViewModel = _autorAppService.GetById(id.Value);

                return View(autorViewModel);
            }
            catch (HttpRequestException e) //Quando vem um status code diferente de "Successful" (200 Ok)
            {
                Console.WriteLine(e);
                return NotFound(e.Message);
            }
            catch (NotSupportedException e) // When content type is not valid
            {
                Console.WriteLine(e);
                return NotFound(e);
            }
            catch (JsonException e) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
                return NotFound(e);
            }
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(AutorViewModel autorViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var autor = _autorAppService.Create(autorViewModel);
                    return RedirectToAction(nameof(Details), new { id = autor.Id });
                }
                catch (JsonException e)
                {
                    return View("Error");
                }

                //return RedirectToAction(nameof(Index));
            }
            return View(autorViewModel);
        }

        // GET: Autor/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorViewModel = _autorAppService.GetById(id.Value);
            if (autorViewModel == null)
            {
                return NotFound();
            }
            return View(autorViewModel);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public IActionResult Edit(int id, [Bind("Id,Nome,UltimoNome,Nascimento")] AutorViewModel autorViewModel)
        {
            if (id != autorViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _autorAppService.Update(autorViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(autorViewModel);
        }

        // GET: Autor/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorViewModel = _autorAppService.GetById(id.Value);
            if (autorViewModel == null)
            {
                return NotFound();
            }

            return View(autorViewModel);
        }

        // POST: Autor/Delete/5
        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _autorAppService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AutorViewModelExists(int id)
        {
            var autorEncontrado = _autorAppService.GetById(id);
            return autorEncontrado != null;
        }
    }
}
