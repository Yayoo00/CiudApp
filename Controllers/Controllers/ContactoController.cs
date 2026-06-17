using CiudApp.Data;
using CiudApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CiudApp.Controllers
{
    public class ContactoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(
            string nombre,
            string correo,
            string asunto,
            string comentario)
        {
            var sugerencia = new Sugerencia
            {
                Nombre = nombre,
                Correo = correo,
                Asunto = asunto,
                Comentario = comentario
            };

            _context.Sugerencias.Add(sugerencia);
            await _context.SaveChangesAsync();

            ViewBag.Mensaje = "✅ Sugerencia enviada correctamente";

            return View();
        }
    }
}