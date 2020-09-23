using Application.AppServices;
using Application.ViewModels;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASDL20M3.Mvc2.Controllers
{
    public abstract class BaseCrudController<TViewModel> : Controller 
        where TViewModel : BaseViewModel
    {
        private readonly IBaseCrudAppService<TViewModel> _baseCrudAppService;

        protected BaseCrudController(
            IBaseCrudAppService<TViewModel> baseCrudAppService)
        {
            _baseCrudAppService = baseCrudAppService;
        }

        public virtual IActionResult Index(
            string searchText)
        {
            var viewModels = _baseCrudAppService.GetAll(searchText);

            ViewBag.SearchText = searchText;

            return View(viewModels);
        }

        public virtual IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _baseCrudAppService.GetById(id.Value);

            return View(viewModel);
        }

        public virtual IActionResult Create()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Create(TViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var autor = _baseCrudAppService.Create(viewModel);
            return RedirectToAction(nameof(Details), new { id = autor.Id });
        }

        public virtual IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _baseCrudAppService.GetById(id.Value);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(int id, TViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _baseCrudAppService.Update(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public virtual IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _baseCrudAppService.GetById(id.Value);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult DeleteConfirmed(int id)
        {
            _baseCrudAppService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
