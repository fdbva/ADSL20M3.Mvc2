﻿using Domain.Model.Interfaces.Services;
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
        public IActionResult OnPost([FromBody] LivroModel livroModel)
        {
            if (ModelState.IsValid)
            {
                var livroCriado = _livroService.Create(livroModel);

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

            var updatedModel = _livroService.Update(livroModel);

            return Ok(updatedModel);

            //TODO: Passar lógica de tratamento de erro para Infrastructure.Data
            //try
            //{
            //    _livroService.Update(livroModel);
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!LivroModelExists(livroModel.Id))
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