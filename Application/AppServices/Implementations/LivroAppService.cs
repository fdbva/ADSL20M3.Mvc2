using System.Collections.Generic;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public class LivroAppService : ILivroAppService
    {
        private readonly ILivroService _livroService;
        private readonly IMapper _autorMapper;
        private readonly IUnitOfWork _unitOfWork;

        public LivroAppService(
            ILivroService livroService,
            IMapper autorMapper,
            IUnitOfWork unitOfWork)
        {
            _livroService = livroService;
            _autorMapper = autorMapper;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<LivroViewModel> GetAll(string searchText)
        {
            var livros = _livroService
                .GetAll(searchText);

            return _autorMapper.Map<IEnumerable<LivroViewModel>>(livros);
        }

        public LivroViewModel GetById(int id)
        {
            var livro = _livroService.GetById(id);

            return _autorMapper.Map<LivroViewModel>(livro);
        }

        public LivroViewModel Create(LivroAutorAggregateRequest livroAutorAggregateRequest)
        {
            var livroAutorAggregateModel = _autorMapper.Map<LivroAutorAggregateModel>(livroAutorAggregateRequest);

            _unitOfWork.BeginTransaction();
            var livro = _livroService.Create(livroAutorAggregateModel);
            _unitOfWork.Commit();

            return _autorMapper.Map<LivroViewModel>(livro);
        }

        public LivroViewModel Update(LivroViewModel livroViewModel)
        {
            var livroModel = _autorMapper.Map<LivroModel>(livroViewModel);

            _unitOfWork.BeginTransaction();
            var livro = _livroService.Update(livroModel);
            _unitOfWork.Commit();

            return _autorMapper.Map<LivroViewModel>(livro);
        }

        public void Delete(int id)
        {
            _unitOfWork.BeginTransaction();
            _livroService.Delete(id);
            _unitOfWork.Commit();
        }

        public bool CheckIsbn(string isbn, int id)
        {
            return _livroService.CheckIsbn(isbn, id);
        }
    }
}
