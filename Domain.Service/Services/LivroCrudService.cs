using System.Collections.Generic;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class LivroCrudService : BaseCrudService<LivroModel>, ILivroCrudService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly ILivroRepository _livroRepository;

        public LivroCrudService(
            IAutorRepository autorRepository,
            ILivroRepository livroRepository) : base(livroRepository)
        {
            _autorRepository = autorRepository;
            _livroRepository = livroRepository;
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

        public bool CheckIsbn(string isbn, int id)
        {
            var livroModel = _livroRepository.GetIsbnNotFromThisId(isbn, id);

            return livroModel is null;
        }
    }
}
