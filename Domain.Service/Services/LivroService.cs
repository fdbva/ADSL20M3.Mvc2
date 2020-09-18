using System.Collections.Generic;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class LivroService : ILivroService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly ILivroRepository _livroRepository;

        public LivroService(
            IAutorRepository autorRepository,
            ILivroRepository livroRepository)
        {
            _autorRepository = autorRepository;
            _livroRepository = livroRepository;
        }

        public IEnumerable<LivroModel> GetAll(
            string searchText)
        {
            return _livroRepository.GetAll(searchText);
        }

        public IEnumerable<LivroModel> GetAll()
        {
            return _livroRepository.GetAll();
        }

        public LivroModel GetById(int id)
        {
            return _livroRepository.GetById(id);
        }

        public LivroModel Create(LivroModel livroModel)
        {
            return _livroRepository.Create(livroModel);
        }

        public LivroModel Create(LivroAutorAggregateModel livroAutorAggregateModel)
        {
            if (livroAutorAggregateModel.Livro.AutorId > 0)
            {
                return _livroRepository.Create(livroAutorAggregateModel.Livro);
            }

            //Exemplo com transaction sem UnitOfWork e SaveChanges nos Repositories
            //using var transactionScope = new TransactionScope(); //TransactionScopeAsyncFlowOption.Enabled);
            var autor = _autorRepository.Create(livroAutorAggregateModel.Autor);

            livroAutorAggregateModel.Livro.Autor = autor; //Com Transaction, usar AutorId

            var livro = _livroRepository.Create(livroAutorAggregateModel.Livro);

            //Exemplo com transaction sem UnitOfWork e SaveChanges nos Repositories
            //transactionScope.Complete();
            return livro;
        }

        public LivroModel Update(LivroModel livroModel)
        {
            return _livroRepository.Update(livroModel);
        }

        public void Delete(int id)
        {
            _livroRepository.Delete(id);
        }

        public bool CheckIsbn(string isbn, int id)
        {
            var livroModel = _livroRepository.GetIsbnNotFromThisId(isbn, id);

            return livroModel is null;
        }
    }
}
