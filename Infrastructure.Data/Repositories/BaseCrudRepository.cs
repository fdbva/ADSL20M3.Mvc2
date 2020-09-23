using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure.Context;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public abstract class BaseCrudRepository<TModel> : IBaseCrudRepository<TModel> where TModel : BaseModel
    {
        protected readonly BibliotecaContext BibliotecaContext;
        protected readonly DbSet<TModel> DbSet;

        protected BaseCrudRepository(
            BibliotecaContext bibliotecaContext)
        {
            BibliotecaContext = bibliotecaContext;
            DbSet = bibliotecaContext.Set<TModel>();
        }

        protected abstract bool Filter(TModel model, string searchText);
        public virtual IEnumerable<TModel> GetAll(
            string searchText = null)
        {
            //não ideal, não está sabendo traduzir o Filter para SQL
            //o ToList é uma correção rápida, trazendo tudo para memória antes de filtrar
            return string.IsNullOrWhiteSpace(searchText)
                ? DbSet
                : DbSet.ToList().Where(x => Filter(x, searchText));
        }

        public virtual TModel GetById(int id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id);
        }

        public virtual TModel Create(TModel model)
        {
            return DbSet.Add(model).Entity;
        }

        public virtual TModel Update(TModel model)
        {
            try
            {
                return DbSet.Update(model).Entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = GetById(model.Id) is null
                    ? $"Entidade {model.GetType().Name} não encontrado na base de dados"
                    : "Ocorreu um erro inesperado de concorrência. Tente novamente.";

                throw new RepositoryException(message, ex);
            }
        }

        public virtual void Delete(int id)
        {
            var model = GetById(id);
            DbSet.Remove(model);
        }
    }
}
