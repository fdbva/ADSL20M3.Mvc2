using System;
using System.Net.Http;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        //Lembrar de registrar dependência no Startup.cs
        public AutorController(
            IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public IActionResult OnGet()
        {
            var todosAutores = _autorService.GetAll();
            return Ok(todosAutores);
        }

        [HttpGet("{id}")]
        public IActionResult OnGet(int? id)
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

            return Ok(autorModel);
        }

        [HttpPost]
        public IActionResult OnPost([FromBody] AutorModel autorModel)
        {
            if (ModelState.IsValid)
            {
                var autorCriado = _autorService.Create(autorModel);

                return Ok(autorCriado);
            }

            return BadRequest("Existe algum valor inválido passado.");
        }

        [HttpPut("{id}")]
        public IActionResult OnPut(int id, AutorModel autorModel)
        {
            if (id != autorModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) 
                return BadRequest("Existe algum valor inválido passado.");

            var updatedModel = _autorService.Update(autorModel);

            return Ok(updatedModel);

            //TODO: Passar lógica de tratamento de erro para Infrastructure.Data
            //try
            //{
            //    _autorService.Update(autorModel);
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AutorModelExists(autorModel.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
        }

        [HttpDelete("{id}")]
        public IActionResult OnDelete(int id)
        {
            _autorService.Delete(id);
            return Ok();
        }
    }
}
