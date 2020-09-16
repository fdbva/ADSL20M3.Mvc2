using System.Collections.Generic;
using Application.ViewModels;

namespace Application.AppServices
{
    public interface IAutorAppService
    {
        IEnumerable<AutorViewModel> GetAll();
        AutorViewModel GetById(int id);
        AutorViewModel Create(AutorViewModel autorModel);
        AutorViewModel Update(AutorViewModel autorModel);
        void Delete(int id);
    }
}
