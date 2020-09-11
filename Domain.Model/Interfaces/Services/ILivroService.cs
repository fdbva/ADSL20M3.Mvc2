using System.Collections.Generic;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface ILivroService
    {
        IEnumerable<LivroModel> GetAll();
        LivroModel GetById(int id);
        LivroModel Create(LivroAutorAggregateModel livroModel);
        LivroModel Update(LivroModel livroModel);
        void Delete(int id);
        bool CheckIsbn(string isbn, int id);
    }
}
