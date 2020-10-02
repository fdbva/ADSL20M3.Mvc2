using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface ILivroCrudService : IBaseCrudService<LivroModel>
    {
        LivroModel Create(LivroAutorAggregateModel livroModel);
        bool CheckIsbn(string isbn, int id);
    }
}
