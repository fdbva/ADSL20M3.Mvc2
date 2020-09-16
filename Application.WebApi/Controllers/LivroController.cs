using System;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        private readonly IUnitOfWork _unitOfWork;

        //Lembrar de registrar dependência no Startup.cs
        public LivroController(
            ILivroService livroService,
            IUnitOfWork unitOfWork)
        {
            _livroService = livroService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{searchText?}")]
        public IActionResult OnGet(string searchText)
        {
            var todosLivros = _livroService.GetAll(searchText);
            return Ok(todosLivros);
        }

        [HttpGet("GetById/{id}")]
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
                _unitOfWork.BeginTransaction();
                var livroCriado = _livroService.Create(livroAggregateModel);
                _unitOfWork.Commit();

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
                _unitOfWork.BeginTransaction();
                var updatedModel = _livroService.Update(livroModel);
                _unitOfWork.Commit();

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
            _unitOfWork.BeginTransaction();
            _livroService.Delete(id);
            _unitOfWork.Commit();

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
