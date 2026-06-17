using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    [Authorize]
    public class VidaUniversitariaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}