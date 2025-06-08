using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Home()
        {
            return View();
        }      
    }
}
