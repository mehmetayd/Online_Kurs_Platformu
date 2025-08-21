using Microsoft.AspNetCore.Mvc;

namespace Online_Kurs_Platformu.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
