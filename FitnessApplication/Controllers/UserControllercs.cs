using Microsoft.AspNetCore.Mvc;

namespace FitnessApplication.Controllers
{
    public class UserControllercs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
