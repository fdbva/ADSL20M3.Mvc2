using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface ILivroRepository : IBaseCrudRepository<LivroModel>
    {
        LivroModel GetIsbnNotFromThisId(string isbn, int id);
    }
}
