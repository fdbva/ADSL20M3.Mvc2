using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure.Context;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.Repositories
{
    //Lembrar de registrar dependência no Startup.cs
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public AutorRepository(
            BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public IEnumerable<AutorModel> GetAll()
        {
            return _bibliotecaContext
                .Autores
                .Include(x => x.Livros)
                .AsEnumerable();
        }

        public AutorModel GetById(int id)
        {
            return _bibliotecaContext
                .Autores
                .Include(x => x.Livros)
                .FirstOrDefault(x => x.Id == id);
        }

        public AutorModel Create(AutorModel autorModel)
        {
            _bibliotecaContext.Add(autorModel);
            _bibliotecaContext.SaveChanges();

            return autorModel;
        }

        public AutorModel Update(AutorModel autorModel)
        {

            //TODO: Passar lógica de tratamento de erro para Infrastructure.Data
            try
            {
                _bibliotecaContext.Update(autorModel);
                _bibliotecaContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = GetById(autorModel.Id) is null
                    ? "Autor não encontrado na base de dados"
                    : "Ocorreu um erro inesperado de concorrência. Tente novamente.";

                throw new RepositoryException(message, ex);
            }

            return autorModel;
        }

        public void Delete(int id)
        {
            var autorModel = GetById(id);
            _bibliotecaContext.Remove(autorModel);
            _bibliotecaContext.SaveChanges();
        }
    }
}
