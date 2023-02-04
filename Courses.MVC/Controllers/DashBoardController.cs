using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Courses.MVC.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IConfiguration _configuration;
        public DashBoardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult DashBoard()
        {
            return View();
        }

        public IActionResult UserDashBoard()
        {
            return View();
        }
    }
}
