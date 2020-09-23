using System.Collections.Generic;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public abstract class BaseCrudService<TModel> : IBaseCrudService<TModel> where TModel : BaseModel
    {
        private readonly IBaseCrudRepository<TModel> _baseCrudRepository;

        protected BaseCrudService(
            IBaseCrudRepository<TModel> baseCrudRepository)
        {
            _baseCrudRepository = baseCrudRepository;
        }

        public virtual TModel Create(TModel model)
        {
            return _baseCrudRepository.Create(model);
        }

        public virtual void Delete(int id)
        {
            _baseCrudRepository.Delete(id);
        }

        public virtual IEnumerable<TModel> GetAll(string searchText)
        {
            return _baseCrudRepository.GetAll(searchText);
        }

        public virtual TModel GetById(int id)
        {
            return _baseCrudRepository.GetById(id);
        }

        public virtual TModel Update(TModel model)
        {
            return _baseCrudRepository.Update(model);
        }
    }
}
