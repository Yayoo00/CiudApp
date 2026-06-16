using CiudApp.ML;
using Microsoft.AspNetCore.Mvc;
using CiudApp.Data;

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
                },
                new
                {
                    id = 3,
                    nombre = "Ruta Parque Kennedy",
                    distrito = "Miraflores",
                    dificultad = "Dificil",
                    puntos = 200
                }
            };

            return Ok(rutas);
        }
        [HttpGet("recompensas")]
public IActionResult ObtenerRecompensas()
{
    var recompensas = new[]
    {
        new
        {
            nombre = "🌱 Insignia Eco",
            puntos = 100
        },
        new
        {
            nombre = "☕ Cupón Cafetería",
            puntos = 250
        }
    };

    return Ok(recompensas);
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