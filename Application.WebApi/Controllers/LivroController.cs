using System;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        //Lembrar de registrar dependência no Startup.cs
        public LivroController(
            ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public IActionResult OnGet()
        {
            var todosLivros = _livroService.GetAll();
            return Ok(todosLivros);
        }

        [HttpGet("{id}")]
        public IActionResult OnGet(int? id)
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

            return Ok(livroModel);
        }

        [HttpPost]
        public IActionResult OnPost([FromBody] LivroAutorAggregateModel livroAggregateModel)
        {
            if (ModelState.IsValid)
            {
                var livroCriado = _livroService.Create(livroAggregateModel);

                return Ok(livroCriado);
            }

            return BadRequest("Existe algum valor inválido passado.");
        }

        [HttpPut("{id}")]
        public IActionResult OnPut(int id, LivroModel livroModel)
        {
            if (id != livroModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest("Existe algum valor inválido passado.");

            try
            {
                var updatedModel = _livroService.Update(livroModel);

                return Ok(updatedModel);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult OnDelete(int id)
        {
            _livroService.Delete(id);
            return Ok();
        }


        [HttpGet("CheckIsbn/{isbn}/{id}")]
        public IActionResult OnGet(string isbn, int id)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return BadRequest("Isbn inválido");

            return Ok(_livroService.CheckIsbn(isbn, id));
        }
    }
}
