using System.Diagnostics;
using FitnessApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly FitnessContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(FitnessContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //gets the first three workouts from the database so they can be shown on the home page
            var featuredWorkouts = _context.Workouts.Take(3).ToList();
            //passes the workouts to the view 
            ViewBag.FeaturedWorkouts = featuredWorkouts;
            return View();
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
