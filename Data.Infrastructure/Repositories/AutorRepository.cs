using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure.Context;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;

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
            return _bibliotecaContext.Autores.AsEnumerable();
        }

        public AutorModel GetById(int id)
        {
            return _bibliotecaContext.Autores.FirstOrDefault(x => x.Id == id);
        }

        public AutorModel Create(AutorModel autorModel)
        {
            _bibliotecaContext.Add(autorModel);
            _bibliotecaContext.SaveChanges();

            return autorModel;
        }

        public AutorModel Update(AutorModel autorModel)
        {
            _bibliotecaContext.Update(autorModel);
            _bibliotecaContext.SaveChanges();

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
