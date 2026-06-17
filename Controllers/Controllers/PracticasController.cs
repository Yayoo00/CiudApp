using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class PracticasController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}