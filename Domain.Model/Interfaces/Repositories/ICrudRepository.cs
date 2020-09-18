using System.Collections.Generic;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface ICrudRepository<TEntityModel> where TEntityModel : BaseModel
    {
        IEnumerable<TEntityModel> GetAll();
        TEntityModel GetById(int id);
        TEntityModel Create(TEntityModel entityModel);
        TEntityModel Update(TEntityModel entityModel);
        void Delete(int id);
    }
}
