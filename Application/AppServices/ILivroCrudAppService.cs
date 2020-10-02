using Application.ViewModels;

namespace Application.AppServices
{
    public interface ILivroCrudAppService : IBaseCrudAppService<LivroViewModel>
    {
        LivroViewModel Create(LivroAutorAggregateRequest livroModel);
        bool CheckIsbn(string isbn, int id);
    }
}
