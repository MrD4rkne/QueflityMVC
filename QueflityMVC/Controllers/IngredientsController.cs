using Microsoft.AspNetCore.Mvc;

namespace QueflityMVC.Web.Controllers
{
    [Route("Ingredients")]
    public class IngredientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
