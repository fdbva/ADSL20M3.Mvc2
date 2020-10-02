using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure.Context;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class LivroCrudRepository : BaseCrudRepository<LivroModel>, ILivroRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public LivroCrudRepository(
            BibliotecaContext bibliotecaContext) : base(bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        protected override bool Filter(LivroModel livroModel, string searchText)
        {
            return livroModel.Titulo.Contains(searchText);
        }

        public override LivroModel GetById(int id)
        {
            return _bibliotecaContext.Livros
                .Include(l => l.Autor)
                .FirstOrDefault(m => m.Id == id);
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
