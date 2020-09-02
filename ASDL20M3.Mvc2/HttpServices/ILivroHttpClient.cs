using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace ASDL20M3.Mvc2.HttpServices
{
    public interface ILivroHttpClient
    {
        Task<IEnumerable<LivroModel>> GetAllAsync();
        Task<LivroModel> GetByIdAsync(int id);
        Task<LivroModel> CreateAsync(LivroModel livroModel);
        Task<LivroModel> UpdateAsync(LivroModel livroModel);
        Task DeleteAsync(int id);
        Task<bool> CheckIsbn(string isbn, int id);
    }
}
