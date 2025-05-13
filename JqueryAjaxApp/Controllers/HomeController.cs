using JqueryAjaxApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JqueryAjaxApp.Controllers
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


        [HttpPost]
        public IActionResult GetData(string name)
        {
            // Örnek veri döndürme
            var response = new { message = $"Merhaba {name}, AJAX isteğiniz başarılı!" };
            return Json(response);
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