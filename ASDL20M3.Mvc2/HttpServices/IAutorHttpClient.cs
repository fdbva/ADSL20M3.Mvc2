using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace ASDL20M3.Mvc2.HttpServices
{
    public interface IAutorHttpClient
    {
        Task<IEnumerable<AutorModel>> GetAllAsync();
        Task<AutorModel> GetByIdAsync(int id);
        Task<AutorModel> CreateAsync(AutorModel autorModel);
        Task<AutorModel> UpdateAsync(AutorModel autorModel);
        Task DeleteAsync(int id);
    }
}
