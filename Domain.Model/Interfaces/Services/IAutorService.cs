using System.Collections.Generic;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IAutorService
    {
        IEnumerable<AutorModel> GetAll();
        AutorModel GetById(int id);
        AutorModel Create(AutorModel autorModel);
        AutorModel Update(AutorModel autorModel);
        void Delete(int id);
    }
}
