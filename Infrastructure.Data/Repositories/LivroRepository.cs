using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure.Context;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class LivroRepository : BaseRepository<LivroModel>, ILivroRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public LivroRepository(
            BibliotecaContext bibliotecaContext) : base(bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public IEnumerable<LivroModel> GetAll(
            string searchText)
        {
            var livros = GetAll();

            return string.IsNullOrWhiteSpace(searchText)
                ? livros
                : livros.Where(x => x.Titulo.Contains(searchText));
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
