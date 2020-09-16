using System;
using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure.Context;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public LivroRepository(
            BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public IEnumerable<LivroModel> GetAll(
            string searchText)
        {
            var livros = _bibliotecaContext
                .Livros
                .Include(l => l.Autor);

            return string.IsNullOrWhiteSpace(searchText)
                ? livros
                : livros.Where(x => x.Titulo == searchText);
        }

        public LivroModel GetById(int id)
        {
            return _bibliotecaContext.Livros
                .Include(l => l.Autor)
                .FirstOrDefault(m => m.Id == id);
        }

        public LivroModel Create(LivroModel livroModel)
        {
            var returnedLivroModel =
            _bibliotecaContext
                .Add(livroModel);

            //TODO: rever create se precisa do include
            //returnedLivroModel
            //    .Reference(x=>x.Autor)
            //    .Load();

            //return returnedLivroModel.Entity;

            return livroModel;
        }

        public LivroModel Update(LivroModel livroModel)
        {
            try
            {
                _bibliotecaContext.Update(livroModel);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = GetById(livroModel.Id) is null
                    ? "Livro não encontrado na base de dados"
                    : "Ocorreu um erro inesperado de concorrência. Tente novamente.";

                throw new RepositoryException(message, ex);
            }

            return livroModel;
        }

        public void Delete(int id)
        {
            var livroModel = GetById(id);
            _bibliotecaContext.Remove(livroModel);
        }

        public LivroModel GetIsbnNotFromThisId(string isbn, int id)
        {
            var result = _bibliotecaContext
                .Livros
                .FirstOrDefault(x => x.Isbn == isbn && x.Id != id);

            return result;
        }
    }
}
