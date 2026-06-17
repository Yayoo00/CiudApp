using CiudApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    [Authorize]
    public class EcoRetosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EcoRetosController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    [HttpPost]
[HttpPost]
public async Task<IActionResult> CompletarReto(
    string nombreReto,
    int puntos)
{
    var user = await _userManager.GetUserAsync(User);

    if (user == null)
        return RedirectToAction("Index");

    var perfil = _context.PerfilesUsuario
        .FirstOrDefault(p => p.UserId == user.Id);

    if (perfil == null)
        return RedirectToAction("Index");

    var ultimoReto = _context.EcoRetosCompletados
        .Where(r => r.UserId == user.Id &&
                    r.NombreReto == nombreReto)
        .OrderByDescending(r => r.FechaCompletado)
        .FirstOrDefault();

    if (ultimoReto != null &&
        ultimoReto.FechaCompletado.AddDays(7) > DateTime.Now)
    {
        TempData["Error"] =
    $"Debes esperar 7 días para volver a completar '{nombreReto}'.";

        return RedirectToAction("Index");
    }

    perfil.Puntos += puntos;
    perfil.RetosCompletados += 1;

    _context.EcoRetosCompletados.Add(
        new Models.EcoRetoCompletado
        {
            UserId = user.Id,
            NombreReto = nombreReto,
            FechaCompletado = DateTime.Now
        });

    _context.SaveChanges();

    TempData["Success"] =
    $"¡Ganaste {puntos} puntos por '{nombreReto}'!";

    return RedirectToAction("Index", "Dashboard");
}

    }
    
}