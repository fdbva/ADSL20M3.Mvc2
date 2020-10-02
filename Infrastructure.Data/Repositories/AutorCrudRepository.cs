using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure.Context;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    //Lembrar de registrar dependência no Startup.cs
    public class AutorCrudRepository : BaseCrudRepository<AutorModel>, IAutorRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public AutorCrudRepository(
            BibliotecaContext bibliotecaContext) : base(bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        protected override bool Filter(AutorModel autorModel, string searchText)
        {
            return autorModel.Nome.Contains(searchText);
        }

        public override AutorModel GetById(int id)
        {
            return _bibliotecaContext
                .Autores
                .Include(x => x.Livros)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
