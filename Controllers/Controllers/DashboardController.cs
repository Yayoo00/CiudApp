using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Puntos = 1240;
            ViewBag.Rutas = 8;
            ViewBag.Retos = 12;
            ViewBag.Nivel = "Intermedio";

            ViewBag.Ranking = new[]
            {
                new { Nombre = "Sofía R.", Puntos = 4820 },
                new { Nombre = "Carlos M.", Puntos = 4310 },
                new { Nombre = User.Identity?.Name ?? "Tú", Puntos = 1240 }
            };

            return View();
        }
    }
}