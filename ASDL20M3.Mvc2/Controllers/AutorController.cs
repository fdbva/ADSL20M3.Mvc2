using Application.AppServices;
using Application.ViewModels;

namespace ASDL20M3.Mvc2.Controllers
{
    public class AutorController : BaseCrudController<AutorViewModel>
    {
        public AutorController(
            IAutorCrudAppService autorCrudAppService) : base(autorCrudAppService)
        {
        }
    }
}
