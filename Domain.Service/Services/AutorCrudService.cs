using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class AutorCrudService : BaseCrudService<AutorModel>, IAutorCrudService
    {
        //Lembrar de registrar dependência no Startup.cs
        public AutorCrudService(
            IAutorRepository autorRepository) : base(autorRepository)
        {
        }
    }
}
