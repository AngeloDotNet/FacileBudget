using Microsoft.AspNetCore.Mvc;

namespace FacileBudget.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Errore";
            return View();
        }
    }
}