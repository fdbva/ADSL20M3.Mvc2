using System.Collections.Generic;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public class AutorAppService : IAutorAppService
    {
        private readonly IAutorService _autorService;
        private readonly IMapper _autorMapper;
        private readonly IUnitOfWork _unitOfWork;

        public AutorAppService(
            IAutorService autorService,
            IMapper autorMapper,
            IUnitOfWork unitOfWork)
        {
            _autorService = autorService;
            _autorMapper = autorMapper;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AutorViewModel> GetAll()
        {
            var autores = _autorService.GetAll();

            return _autorMapper.Map<IEnumerable<AutorViewModel>>(autores);
        }

        public AutorViewModel GetById(int id)
        {
            var autor = _autorService.GetById(id);

            return _autorMapper.Map<AutorViewModel>(autor);
        }

        public AutorViewModel Create(AutorViewModel autorViewModel)
        {
            var autorModel = _autorMapper.Map<AutorModel>(autorViewModel);

            _unitOfWork.BeginTransaction();
            var autorModelCreated = _autorService.Create(autorModel);
            _unitOfWork.Commit();

            return _autorMapper.Map<AutorViewModel>(autorModelCreated);
        }

        public AutorViewModel Update(AutorViewModel autorViewModel)
        {
            var autorModel = _autorMapper.Map<AutorModel>(autorViewModel);

            _unitOfWork.BeginTransaction();
            var autorModelUpdated = _autorService.Update(autorModel);
            _unitOfWork.Commit();

            return _autorMapper.Map<AutorViewModel>(autorModelUpdated);
        }

        public void Delete(int id)
        {
            _unitOfWork.BeginTransaction();
            _autorService.Delete(id);
            _unitOfWork.Commit();
        }
    }
}
