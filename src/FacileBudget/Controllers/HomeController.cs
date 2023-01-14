using Microsoft.AspNetCore.Mvc;

namespace FacileBudget.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Facile Budget";
            return View();
        }
    }
}