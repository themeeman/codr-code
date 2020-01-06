using Microsoft.AspNetCore.Mvc;

namespace Codr.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            if (Request.Cookies.ContainsKey("Session")) {
                return Redirect("/App");
            }
            return View();
        }
    }
}
