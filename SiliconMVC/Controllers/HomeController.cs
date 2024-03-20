using Microsoft.AspNetCore.Mvc;
using SiliconMVC.Model.Views;

namespace SiliconMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: /User/
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            ViewData["Title"] = "Home Page";

            return View(viewModel);
            
        }

        
    }
}
