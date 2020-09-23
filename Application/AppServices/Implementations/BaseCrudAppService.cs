using System.Collections.Generic;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public abstract class BaseCrudAppService<TModel, TViewModel> : IBaseCrudAppService<TViewModel>
        where TModel : BaseModel
        where TViewModel : BaseViewModel
    {
        private readonly IBaseCrudService<TModel> _baseCrudService;
        private readonly IMapper _autoMapper;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseCrudAppService(
            IBaseCrudService<TModel> baseCrudService,
            IMapper autoMapper,
            IUnitOfWork unitOfWork)
        {
            _baseCrudService = baseCrudService;
            _autoMapper = autoMapper;
            _unitOfWork = unitOfWork;
        }

        public virtual IEnumerable<TViewModel> GetAll(string searchText)
        {
            var models = _baseCrudService.GetAll(searchText);

            return _autoMapper.Map<IEnumerable<TViewModel>>(models);
        }

        public virtual TViewModel GetById(int id)
        {
            var model = _baseCrudService.GetById(id);

            return _autoMapper.Map<TViewModel>(model);
        }

        public virtual TViewModel Create(TViewModel viewModel)
        {
            var model = _autoMapper.Map<TModel>(viewModel);

            _unitOfWork.BeginTransaction();
            var createdModel = _baseCrudService.Create(model);
            _unitOfWork.Commit();

            return _autoMapper.Map<TViewModel>(createdModel);
        }

        public virtual TViewModel Update(TViewModel viewModel)
        {
            var model = _autoMapper.Map<TModel>(viewModel);

            _unitOfWork.BeginTransaction();
            var updatedModel = _baseCrudService.Update(model);
            _unitOfWork.Commit();

            return _autoMapper.Map<TViewModel>(updatedModel);
        }

        public virtual void Delete(int id)
        {
            _unitOfWork.BeginTransaction();
            _baseCrudService.Delete(id);
            _unitOfWork.Commit();
        }
    }
}
