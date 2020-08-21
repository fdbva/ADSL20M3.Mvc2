using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;

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
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44365/")
            };

            //Api antiga de GetAsync
            //var getResponse = await httpClient.GetAsync("api/autor");

            //var content = getResponse.Content;
            //var contentString = await content.ReadAsStringAsync();
            //var autores = JsonSerializer.Deserialize<IEnumerable<AutorModel>>(contentString);

            //Tem algum detalhe errado ainda na opção abaixo
            //var stream = await content.ReadAsStreamAsync();
            //var autores = await JsonSerializer
            //    .DeserializeAsync<IEnumerable<AutorModel>>(stream);

            var response = await httpClient
                .GetFromJsonAsync<IEnumerable<AutorModel>>(
                    "api/autor");

            return View(response);
        }

        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44365/")
            };

            try
            {
                var autorModel = await httpClient
                    .GetFromJsonAsync<AutorModel>(
                        $"api/autor/{id.Value}");

                return View(autorModel);
            }
            catch (HttpRequestException e) //Quando vem um status code diferente de "Successful" (200 Ok)
            {
                Console.WriteLine(e);
                return NotFound(e);
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
        public async Task<IActionResult> Create(AutorModel autorModel)
        {
            if (ModelState.IsValid)
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:44365/")
                };
                var httpResponseMessage = await httpClient
                    .PostAsJsonAsync<AutorModel>(
                        $"api/autor/", autorModel);

                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                try
                {
                    var autorResponse = await JsonSerializer
                        .DeserializeAsync<AutorModel>(
                            contentStream, 
                            new JsonSerializerOptions
                            {
                                IgnoreNullValues = true, PropertyNameCaseInsensitive = true
                            });
                }
                catch (JsonException) // Invalid JSON
                {
                    Console.WriteLine("Invalid JSON.");
                }

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
        //[Authorize]
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
        //[Authorize]
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
