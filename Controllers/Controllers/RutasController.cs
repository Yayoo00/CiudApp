using CiudApp.Data;
using CiudApp.ML;
using CiudApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CiudApp.Controllers
{
    [Authorize]
    public class RutasController : Controller
    {
        private readonly RutaMLService _mlService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RutasController(RutaMLService mlService, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _mlService = mlService;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var rutas = new[]
            {
                new { Id = 1, Nombre = "Ruta Costa Verde", Distrito = "Miraflores", Distancia = 7f, Puntos = 180 },
                new { Id = 2, Nombre = "Ruta Campo de Marte", Distrito = "Jesús María", Distancia = 3f, Puntos = 90 },
                new { Id = 3, Nombre = "Ruta Parque Kennedy", Distrito = "Miraflores", Distancia = 4.5f, Puntos = 120 }
            };

            ViewBag.Rutas = rutas.Select(r => new
            {
                r.Id,
                r.Nombre,
                r.Distrito,
                r.Distancia,
                r.Puntos,
                Nivel = _mlService.PredecirNivel(r.Distancia)
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Completar(int id)
        {
            var rutas = new[]
            {
                new { Id = 1, Puntos = 180 },
                new { Id = 2, Puntos = 90 },
                new { Id = 3, Puntos = 120 }
            };

            var ruta = rutas.FirstOrDefault(r => r.Id == id);
            if (ruta == null) return NotFound();

            var userId = _userManager.GetUserId(User);

            var perfil = await _context.PerfilesUsuario
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (perfil == null)
            {
                perfil = new PerfilUsuario
                {
                    UserId = userId!,
                    Nombre = "Estudiante",
                    Universidad = "USMP",
                    Distrito = "Lima"
                };

                _context.PerfilesUsuario.Add(perfil);
            }

            perfil.Puntos += ruta.Puntos;
            perfil.RutasCompletadas += 1;

            perfil.Nivel = perfil.Puntos switch
            {
                < 200 => "Inicial",
                < 500 => "Intermedio",
                < 1000 => "Avanzado",
                _ => "Eco Master"
            };

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = $"Ruta completada. Ganaste {ruta.Puntos} puntos.";

            return RedirectToAction("Index", "Dashboard");
        }
    }
}