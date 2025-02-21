using Microsoft.AspNetCore.Mvc;

namespace shoeVerse.Controllers
{
    public class ProductController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
