using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;

namespace Application.AppServices.Implementations
{
    public class LivroAppService : ILivroAppService
    {
        private readonly ILivroService _livroService;
        private readonly IMapper _autorMapper;

        public LivroAppService(
            ILivroService livroService,
            IMapper autorMapper)
        {
            _livroService = livroService;
            _autorMapper = autorMapper;
        }

        public IEnumerable<LivroViewModel> GetAll(string searchText)
        {
            var livros = _livroService
                .GetAll(searchText);

            return _autorMapper.Map<IEnumerable<LivroViewModel>>(livros);
        }

        public LivroViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public LivroViewModel Create(LivroAutorAggregateRequest livroModel)
        {
            throw new NotImplementedException();
        }

        public LivroViewModel Update(LivroViewModel livroModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool CheckIsbn(string isbn, int id)
        {
            throw new NotImplementedException();
        }
    }
}
