using System.Collections.Generic;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(
            ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
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
