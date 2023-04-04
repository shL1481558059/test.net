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
            ViewBag.thisPath = AppDomain.CurrentDomain.BaseDirectory;
            ViewBag.s = path.Remove(path.LastIndexOf("/"));
            ViewBag.dir = Directory.EnumerateDirectories(path).Select(
                p=>p.Replace("\\","/")
                ).ToList();


            ViewBag.file = Directory.EnumerateFiles(path).Select(p => p.Replace(path,"").Replace("\\", "/")).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult FileUpload(IFormFile file)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + $"wwwroot/{DateTime.Now:yyyyMMdd_HHmmssfff}.png";
                var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file.CopyTo(fs);
                fs.Close();
                return Ok("上传成功");
            }
            catch (Exception e)
            {
                return Ok("上传失败\r\n"+e.Message);
                throw;
            }
           
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