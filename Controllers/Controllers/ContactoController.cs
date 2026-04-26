using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    public class ContactoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}