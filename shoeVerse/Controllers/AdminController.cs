using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace shoeVerse.Controllers
{
    [Authorize(Roles = "Administrator")] // Restrict access to admin role only
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
