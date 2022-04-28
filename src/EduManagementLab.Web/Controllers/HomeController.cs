using Microsoft.AspNetCore.Mvc;

namespace EduManagementLab.Web.Controllers
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
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
        public IActionResult Login()
        {
            return LocalRedirect("http://localhost:5001");
        }
    }
}
