using CiudApp.ML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    [Authorize]
    public class RutasController : Controller
    {
        private readonly RutaMLService _mlService;

        public RutasController(RutaMLService mlService)
        {
            _mlService = mlService;
        }

        public IActionResult Index()
        {
            var rutas = new[]
            {
                new { Nombre = "Ruta Costa Verde", Distrito = "Miraflores", Distancia = 7f, Puntos = 180 },
                new { Nombre = "Ruta Campo de Marte", Distrito = "Jesús María", Distancia = 3f, Puntos = 90 },
                new { Nombre = "Ruta Parque Kennedy", Distrito = "Miraflores", Distancia = 4.5f, Puntos = 120 }
            };

            ViewBag.Rutas = rutas.Select(r => new
            {
                r.Nombre,
                r.Distrito,
                r.Distancia,
                r.Puntos,
                Nivel = _mlService.PredecirNivel(r.Distancia)
            });

            return View();
        }
    }
}