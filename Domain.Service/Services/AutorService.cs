using System.Collections.Generic;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        //Lembrar de registrar dependência no Startup.cs
        public AutorService(
            IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        /// <summary>
        /// Exemplo de documentação para Create.
        /// Ao criar, o banco vai gerar o Id via Identity (Sequence).
        /// </summary>
        /// <param name="autorModel">Deve vir sem Id preenchido.</param>
        /// <returns>AutorModel original com o Id adicionado.</returns>
        public AutorModel Create(AutorModel autorModel)
        {
            return _autorRepository.Create(autorModel);
        }

        public void Delete(int id)
        {
            _autorRepository.Delete(id);
        }

        public IEnumerable<AutorModel> GetAll()
        {
            return _autorRepository.GetAll();
        }

        public AutorModel GetById(int id)
        {
            return _autorRepository.GetById(id);
        }

        public AutorModel Update(AutorModel autorModel)
        {
            return _autorRepository.Update(autorModel);
        }
    }
}
