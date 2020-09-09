using System.Collections.Generic;
using System.Threading.Tasks;
using ASDL20M3.Mvc2.Models;

namespace ASDL20M3.Mvc2.HttpServices
{
    public interface IAutorHttpClient
    {
        Task<IEnumerable<AutorViewModel>> GetAllAsync();
        Task<AutorViewModel> GetByIdAsync(int id);
        Task<AutorViewModel> CreateAsync(AutorViewModel autorModel);
        Task<AutorViewModel> UpdateAsync(AutorViewModel autorModel);
        Task DeleteAsync(int id);
    }
}
