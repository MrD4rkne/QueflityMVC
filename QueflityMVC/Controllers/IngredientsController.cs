using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Services;

namespace QueflityMVC.Web.Controllers
{
    [Route("Ingredients")]
    public class IngredientsController : Controller
    {
        private const int DEFAULT_PAGE_SIZE = 2;

        private IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public IActionResult Index()
        {
            return Index(null,string.Empty, DEFAULT_PAGE_SIZE, 1);
        }

        [HttpPost]
        public IActionResult Index(int? itemId,string nameFilter, int pageSize, int pageIndex)
        {
            if (nameFilter == null)
                nameFilter = string.Empty;
            if (pageSize <= 1)
                pageSize = 2;
            if (pageIndex < 1)
                pageIndex = 1;

            var listVm = _ingredientService.GetFilteredList(itemId,nameFilter, pageSize, pageIndex);
            return View(listVm);
        }
    }
}
