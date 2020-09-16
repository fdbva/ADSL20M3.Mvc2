using System.Collections.Generic;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        IEnumerable<LivroModel> GetAll(
            string searchText);
        LivroModel GetById(int id);
        LivroModel Create(LivroModel livroModel);
        LivroModel Update(LivroModel livroModel);
        void Delete(int id);
        LivroModel GetIsbnNotFromThisId(string isbn, int id);
    }
}
