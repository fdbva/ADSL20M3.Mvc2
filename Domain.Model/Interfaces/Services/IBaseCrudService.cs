using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IBaseCrudService<TModel> : IBaseCrudRepository<TModel> where TModel : BaseModel
    {
    }
}
