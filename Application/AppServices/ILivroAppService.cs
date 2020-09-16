using System.Collections.Generic;
using Application.ViewModels;

namespace Application.AppServices
{
    public interface ILivroAppService
    {
        IEnumerable<LivroViewModel> GetAll(
            string searchText);
        LivroViewModel GetById(int id);
        LivroViewModel Create(LivroAutorAggregateRequest livroModel);
        LivroViewModel Update(LivroViewModel livroModel);
        void Delete(int id);
        bool CheckIsbn(string isbn, int id);
    }
}
