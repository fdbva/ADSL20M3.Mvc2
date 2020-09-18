using System.Collections.Generic;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface ILivroRepository : ICrudRepository<LivroModel>
    {
        IEnumerable<LivroModel> GetAll(string searchText);
        LivroModel GetIsbnNotFromThisId(string isbn, int id);
    }
}
