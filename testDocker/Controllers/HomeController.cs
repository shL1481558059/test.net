using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using testDocker.Models;

namespace testDocker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string? path)
        {
            path ??= "/";
            ViewBag.s = path;
            ViewBag.dir = Directory.EnumerateDirectories(path).Select(p=>p.Replace("\\","/")).ToList();
            ViewBag.file = Directory.EnumerateFiles(path).ToList();

            return View();
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