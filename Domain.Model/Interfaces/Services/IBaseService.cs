using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IBaseService<TEntityModel> : ICrudRepository<TEntityModel> where TEntityModel : BaseModel
    {
    }
}
