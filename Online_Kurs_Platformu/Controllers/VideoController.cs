using Microsoft.AspNetCore.Mvc;

namespace Online_Kurs_Platformu.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
