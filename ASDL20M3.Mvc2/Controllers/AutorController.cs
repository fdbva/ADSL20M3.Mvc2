﻿using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ASDL20M3.Mvc2.HttpServices;
using ASDL20M3.Mvc2.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASDL20M3.Mvc2.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorHttpClient _autorHttpClient;

        //Lembrar de registrar dependência no Startup.cs
        public AutorController(
            IAutorHttpClient autorHttpClient)
        {
            _autorHttpClient = autorHttpClient;
        }

        // GET: Autor
        public async Task<IActionResult> Index()
        {
            var autores = await _autorHttpClient.GetAllAsync();

            return View(autores);
        }

        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var autorViewModel = await _autorHttpClient.GetByIdAsync(id.Value);

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
        public async Task<IActionResult> Create(AutorViewModel autorViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var autor = await _autorHttpClient.CreateAsync(autorViewModel);
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorViewModel = await _autorHttpClient.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,UltimoNome,Nascimento")] AutorViewModel autorViewModel)
        {
            if (id != autorViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _autorHttpClient.UpdateAsync(autorViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(autorViewModel);
        }

        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorViewModel = await _autorHttpClient.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _autorHttpClient.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AutorViewModelExists(int id)
        {
            var autorEncontrado = await _autorHttpClient.GetByIdAsync(id);
            return autorEncontrado != null;
        }
    }
}
