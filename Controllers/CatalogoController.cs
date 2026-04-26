using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CiudApp.Controllers
{
    [Authorize] 
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}