using Microsoft.AspNetCore.Mvc;

namespace Codr.Controllers {
    public class LogoutController : Controller {
        public IActionResult Index() {
            Response.Cookies.Delete("Session");
            return Redirect("/");
        }
    }
}
