using Microsoft.AspNetCore.Mvc;
using CiudApp.Services;

namespace CiudApp.Controllers
{
    [ApiController]
    [Route("api/asistente")]
    public class AsistenteApiController : ControllerBase
    {
        private readonly IAService _iaService;

        public AsistenteApiController(IAService iaService)
        {
            _iaService = iaService;
        }

        [HttpPost("preguntar")]
        public async Task<IActionResult> Preguntar([FromBody] PreguntaRequest request)
        {
            var respuesta = await _iaService.PreguntarAsync(request.Pregunta);

            return Ok(new
            {
                respuesta
            });
        }
    }

    public class PreguntaRequest
    {
        public string Pregunta { get; set; } = "";
    }
}