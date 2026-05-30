using CiudApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var recompensas = _context.Recompensas.ToList();
            return View(recompensas);
        }
        [HttpPost]
public IActionResult Eliminar(int id)
{
    var recompensa = _context.Recompensas.Find(id);

    if (recompensa != null)
    {
        _context.Recompensas.Remove(recompensa);
        _context.SaveChanges();
    }

    return RedirectToAction("Index");
}
[HttpPost]
public IActionResult Crear(string nombre, string descripcion, int costoPuntos)
{
    _context.Recompensas.Add(new Models.Recompensa
    {
        Nombre = nombre,
        Descripcion = descripcion,
        CostoPuntos = costoPuntos
    });

    _context.SaveChanges();

    return RedirectToAction("Index");
}
    }
}