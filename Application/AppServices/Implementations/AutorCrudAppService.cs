using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public class AutorCrudAppService : BaseCrudAppService<AutorModel, AutorViewModel>, IAutorCrudAppService
    {
        public AutorCrudAppService(
            IAutorCrudService autorCrudService,
            IMapper autoMapper,
            IUnitOfWork unitOfWork) : base(autorCrudService, autoMapper, unitOfWork)
        {
        }
    }
}
