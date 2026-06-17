using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CiudApp.Services;

namespace CiudApp.Controllers
{
    [Authorize]
    public class AsistenteController : Controller
    {
        private readonly IAService _iaService;

        public AsistenteController(IAService iaService)
        {
            _iaService = iaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string pregunta)
        {
            var respuesta = await _iaService.PreguntarAsync(pregunta);

            ViewBag.Respuesta = respuesta;

            return View();
        }
    }
}