using Microsoft.AspNetCore.Mvc;

namespace testpythonaspng.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
