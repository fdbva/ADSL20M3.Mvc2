using System.Collections.Generic;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IBaseCrudRepository<TModel> where TModel : BaseModel
    {
        IEnumerable<TModel> GetAll(string searchText = null);
        TModel GetById(int id);
        TModel Create(TModel model);
        TModel Update(TModel model);
        void Delete(int id);
    }
}
