using CiudApp.Data;
using CiudApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CiudApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
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
                    Distrito = "Lima",
                    Nivel = "Inicial",
                    Puntos = 0,
                    RutasCompletadas = 0,
                    RetosCompletados = 0
                };

                _context.PerfilesUsuario.Add(perfil);
                await _context.SaveChangesAsync();
            }

            ViewBag.Puntos = perfil.Puntos;
            ViewBag.Rutas = perfil.RutasCompletadas;
            ViewBag.Retos = perfil.RetosCompletados;
            ViewBag.Nivel = perfil.Nivel;

            ViewBag.Ranking = new[]
            {
                new { Nombre = "Sofía R.", Puntos = 4820 },
                new { Nombre = "Carlos M.", Puntos = 4310 },
                new { Nombre = perfil.Nombre, Puntos = perfil.Puntos }
            };

            return View();
        }
    }
}