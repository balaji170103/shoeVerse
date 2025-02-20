using Microsoft.AspNetCore.Mvc;

namespace shoeVerse.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
