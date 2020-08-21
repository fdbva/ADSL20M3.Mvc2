using System.Collections.Generic;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IAutorService
    {
        IEnumerable<AutorModel> GetAll();
        AutorModel GetById(int id);

        /// <summary>
        /// Exemplo de documentação para Create.
        /// Ao criar, o banco vai gerar o Id via Identity (Sequence).
        /// </summary>
        /// <param name="autorModel">Deve vir sem Id preenchido.</param>
        /// <returns>AutorModel original com o Id adicionado.</returns>
        AutorModel Create(AutorModel autorModel);
        AutorModel Update(AutorModel autorModel);
        void Delete(int id);
    }
}
