using CiudApp.Data;
using CiudApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CiudApp.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PerfilController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
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
                    Puntos = 0
                };

                _context.PerfilesUsuario.Add(perfil);
                await _context.SaveChangesAsync();
            }

            return View(perfil);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PerfilUsuario perfil)
        {
            if (!ModelState.IsValid)
            {
                return View(perfil);
            }

            var userId = _userManager.GetUserId(User);

            var perfilDb = await _context.PerfilesUsuario
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (perfilDb == null)
            {
                return NotFound();
            }

            perfilDb.Nombre = perfil.Nombre;
            perfilDb.Universidad = perfil.Universidad;
            perfilDb.Distrito = perfil.Distrito;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}