using CiudApp.ML;
using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly RutaMLService _mlService;

        public ApiController(RutaMLService mlService)
        {
            _mlService = mlService;
        }

        [HttpGet("rutas")]
        public IActionResult ObtenerRutas()
        {
            var rutas = new[]
            {
                new
                {
                    id = 1,
                    nombre = "Ruta Costa Verde",
                    distrito = "Miraflores",
                    dificultad = "Media",
                    puntos = 150
                },
                new
                {
                    id = 2,
                    nombre = "Ruta Campo de Marte",
                    distrito = "Jesús María",
                    dificultad = "Fácil",
                    puntos = 80
                }
            };

            return Ok(rutas);
        }

        [HttpGet("clasificar")]
        public IActionResult ClasificarRuta(float distancia)
        {
            var nivel = _mlService.PredecirNivel(distancia);

            return Ok(new
            {
                distancia,
                nivel
            });
        }
    }
}