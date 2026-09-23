using Microsoft.AspNetCore.Mvc;

namespace Apps.Controllers
{
    public class HomeApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
