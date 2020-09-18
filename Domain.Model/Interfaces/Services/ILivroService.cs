using System.Collections.Generic;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface ILivroService : IBaseService<LivroModel>
    {
        LivroModel Create(LivroAutorAggregateModel livroModel);
        IEnumerable<LivroModel> GetAll(
            string searchText);
        bool CheckIsbn(string isbn, int id);
    }
}
