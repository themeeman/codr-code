using Codr.Data;
using Microsoft.AspNetCore.Mvc;

namespace Codr.Controllers {
    public class LoginController : Controller {
        public IActionResult Index() {
            Response.Cookies.Append("bruh", "bruh");
            return View();
        }

        [HttpGet]
        public IActionResult Process() {
            return Redirect("/Login");
        }

        [HttpPost]
        public IActionResult Process(string email, string password) {
            using var sesh = new UserProvider(DocumentStoreHolder.Store.OpenSession());
            return Json(sesh.GetUserByEmail(email)?.Password.Verify(password));
        }
    }
}
