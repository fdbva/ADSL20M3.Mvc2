using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public class LivroCrudAppService : BaseCrudAppService<LivroModel, LivroViewModel>, ILivroCrudAppService
    {
        private readonly ILivroCrudService _livroCrudService;
        private readonly IMapper _autoMapper;
        private readonly IUnitOfWork _unitOfWork;

        public LivroCrudAppService(
            ILivroCrudService livroCrudService,
            IMapper autoMapper,
            IUnitOfWork unitOfWork) : base(livroCrudService, autoMapper, unitOfWork)
        {
            _livroCrudService = livroCrudService;
            _autoMapper = autoMapper;
            _unitOfWork = unitOfWork;
        }

        public LivroViewModel Create(LivroAutorAggregateRequest livroAutorAggregateRequest)
        {
            var livroAutorAggregateModel = _autoMapper.Map<LivroAutorAggregateModel>(livroAutorAggregateRequest);

            _unitOfWork.BeginTransaction();
            var livro = _livroCrudService.Create(livroAutorAggregateModel);
            _unitOfWork.Commit();

            return _autoMapper.Map<LivroViewModel>(livro);
        }

        public bool CheckIsbn(string isbn, int id)
        {
            return _livroCrudService.CheckIsbn(isbn, id);
        }
    }
}
