using DatabaseFirstEFCore6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DatabaseFirstEFCore6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CodeFirstDBContext context;

        public HomeController(ILogger<HomeController> logger, CodeFirstDBContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var StdData = context.Students.ToList();
            return View(StdData);
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
