using Microsoft.AspNetCore.Mvc;

namespace Codr.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
