using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure.Context;
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

        public IEnumerable<LivroModel> GetAll()
        {
            return _bibliotecaContext
                .Livros
                .Include(l => l.Autor);
        }

        public LivroModel GetById(int id)
        {
            return _bibliotecaContext.Livros
                .Include(l => l.Autor)
                .FirstOrDefault(m => m.Id == id);
        }

        public LivroModel Create(LivroModel livroModel)
        {
            _bibliotecaContext.Add(livroModel);
            _bibliotecaContext.SaveChanges();

            return livroModel;
        }

        public LivroModel Update(LivroModel livroModel)
        {
            _bibliotecaContext.Update(livroModel);
            _bibliotecaContext.SaveChanges();

            return livroModel;
        }

        public void Delete(int id)
        {
            var livroModel = GetById(id);
            _bibliotecaContext.Remove(livroModel);
            _bibliotecaContext.SaveChanges();
        }
    }
}
