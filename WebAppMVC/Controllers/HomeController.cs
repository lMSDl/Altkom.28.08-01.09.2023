using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMVC.Model;

namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Razor(string name, string @string)
        {
            ViewData["name"] = name;
			ViewData[name] = @string;

            ViewBag.Name = name;
            ViewBag.Kawa = @string;


			return View((object)name);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}