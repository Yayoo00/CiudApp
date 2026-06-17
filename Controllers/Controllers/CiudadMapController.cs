using CiudApp.Data;
using CiudApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    [Authorize]
    public class CiudadMapController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CiudadMapController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
        var reportes = _context.ReportesCiudad
            .OrderByDescending(r => r.Fecha)
            .Take(6)
            .ToList();

        ViewBag.Usuarios = _context.PerfilesUsuario
            .ToDictionary(p => p.UserId, p => p.Nombre);

        return View(reportes);
        }

        [HttpGet]
        public IActionResult Reportar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reportar(
            string tipo,
            string ubicacion,
            string descripcion)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Index");

            _context.ReportesCiudad.Add(new ReporteCiudad
            {
                UserId = user.Id,
                Tipo = tipo,
                Ubicacion = ubicacion,
                Descripcion = descripcion,
                Fecha = DateTime.Now
            });

            _context.SaveChanges();

            TempData["Success"] = "Reporte registrado correctamente";

            return RedirectToAction("Index");
        }
    }
}