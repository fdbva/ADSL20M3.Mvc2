using System.Collections.Generic;
using Application.ViewModels;

namespace Application.AppServices
{
    public interface IBaseCrudAppService<TViewModel>
        where TViewModel : BaseViewModel
    {
        IEnumerable<TViewModel> GetAll(string searchText = null);
        TViewModel GetById(int id);
        TViewModel Create(TViewModel viewModel);
        TViewModel Update(TViewModel viewModel);
        void Delete(int id);
    }
}
