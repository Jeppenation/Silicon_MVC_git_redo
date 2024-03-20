using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    public class DownloadsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Downloads";
            return View();
        }
    }
}
