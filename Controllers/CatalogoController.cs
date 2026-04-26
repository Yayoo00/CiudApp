using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}