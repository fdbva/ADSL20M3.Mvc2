using System.Collections.Generic;
using System.Threading.Tasks;
using ASDL20M3.Mvc2.Models;

namespace ASDL20M3.Mvc2.HttpServices
{
    public interface ILivroHttpClient
    {
        Task<IEnumerable<LivroViewModel>> GetAllAsync(
            string searchText);
        Task<LivroViewModel> GetByIdAsync(int id);
        Task<LivroViewModel> CreateAsync(LivroAutorAggregateRequest livroAutorAggregateRequest);
        Task<LivroViewModel> UpdateAsync(LivroViewModel livroModel);
        Task DeleteAsync(int id);
        Task<bool> CheckIsbn(string isbn, int id);
    }
}
