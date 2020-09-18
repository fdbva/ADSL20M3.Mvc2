using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure.Context;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntityModel> : ICrudRepository<TEntityModel> where TEntityModel : BaseModel
    {
        protected readonly BibliotecaContext BibliotecaContext;
        protected readonly DbSet<TEntityModel> DbSet;

        public BaseRepository(
            BibliotecaContext bibliotecaContext)
        {
            BibliotecaContext = bibliotecaContext;
            DbSet = bibliotecaContext.Set<TEntityModel>();
        }

        public virtual IEnumerable<TEntityModel> GetAll()
        {
            return DbSet;
        }

        public virtual TEntityModel GetById(int id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id);
        }

        public virtual TEntityModel Create(TEntityModel entityModel)
        {
            return DbSet.Add(entityModel).Entity;
        }

        public virtual TEntityModel Update(TEntityModel entityModel)
        {
            try
            {
                return DbSet.Update(entityModel).Entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = GetById(entityModel.Id) is null
                    ? $"Entidade {entityModel.GetType().Name} não encontrado na base de dados"
                    : "Ocorreu um erro inesperado de concorrência. Tente novamente.";

                throw new RepositoryException(message, ex);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            DbSet.Remove(entity);
        }
    }
}
