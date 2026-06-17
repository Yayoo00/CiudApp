using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class EventosController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}