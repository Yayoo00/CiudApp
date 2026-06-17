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

    var ranking = _context.PerfilesUsuario
        .Where(p => p.Nombre != "Admin")
        .OrderByDescending(p => p.Puntos)
        .Take(10)
        .Select(p => new
        {
            Nombre = p.Nombre,
            Puntos = p.Puntos
        })
        .ToList();

    ViewBag.Ranking = ranking;

    return View();
}
    }
}