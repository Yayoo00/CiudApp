using CiudApp.Data;
using CiudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace CiudApp.Controllers
{
[Authorize]
public class CatalogoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CatalogoController(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        if (!_context.Recompensas.Any())
        {
            _context.Recompensas.AddRange(
                new Recompensa
                {
                    Nombre = "🌱 Insignia Eco",
                    Descripcion = "Reconocimiento ecológico",
                    CostoPuntos = 100
                },
                new Recompensa
                {
                    Nombre = "☕ Cupón Cafetería",
                    Descripcion = "Descuento en cafetería",
                    CostoPuntos = 250
                },
                new Recompensa
                {
                    Nombre = "📚 Kit de Estudio",
                    Descripcion = "Material académico",
                    CostoPuntos = 500
                },
                new Recompensa
                {
                    Nombre = "👕 Polo CiudApp",
                    Descripcion = "Merchandising oficial",
                    CostoPuntos = 1000
                }
            );

            _context.SaveChanges();
        }

        return View(_context.Recompensas.ToList());
    }

    [HttpPost]
    public async Task<IActionResult> Canjear(int id)
{
    var user = await _userManager.GetUserAsync(User);

    if (user == null)
        return RedirectToAction("Index");

    var perfil = _context.PerfilesUsuario
        .FirstOrDefault(p => p.UserId == user.Id);

    var recompensa = _context.Recompensas
        .FirstOrDefault(r => r.Id == id);

    if (perfil == null || recompensa == null)
        return RedirectToAction("Index");

    if (perfil.Puntos < recompensa.CostoPuntos)
    {
        TempData["Error"] = "No tienes suficientes puntos.";
        return RedirectToAction("Index");
    }

    var ultimoCanje = _context.Canjes
        .Where(c => c.UserId == user.Id &&
                    c.RecompensaId == recompensa.Id)
        .OrderByDescending(c => c.FechaCanje)
        .FirstOrDefault();

    if (ultimoCanje != null &&
        ultimoCanje.FechaCanje.AddDays(7) > DateTime.Now)
    {
        TempData["Error"] =
            "Debes esperar 7 días para volver a canjear esta recompensa.";

        return RedirectToAction("Index");
    }

    perfil.Puntos -= recompensa.CostoPuntos;

    _context.Canjes.Add(new Canje
    {
        UserId = user.Id,
        RecompensaId = recompensa.Id,
        FechaCanje = DateTime.Now
    });

    _context.SaveChanges();

    TempData["Success"] =
        $"Canjeaste: {recompensa.Nombre}";

    return RedirectToAction("Index");
}
}
}