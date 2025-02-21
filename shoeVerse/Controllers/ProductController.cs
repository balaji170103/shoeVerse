using Microsoft.AspNetCore.Mvc;

namespace shoeVerse.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
