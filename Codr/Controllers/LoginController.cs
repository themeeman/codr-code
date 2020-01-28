using Codr.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Codr.Controllers {
    public class LoginController : Controller {
        public IActionResult Index() {
            if (Request.Cookies.ContainsKey("Session")) {
                return Redirect("/App");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Process() {
            return Redirect("/Login");
        }

        [HttpPost]
        public IActionResult Process(string email, string password) {
            using var sesh = new UserProvider(DocumentStoreHolder.Store.OpenSession());
            var user = sesh.GetUserByEmail(email);
            if (user is { } u && u.Password.Verify(password)) {
                u.NewSession();
                sesh.Session.SaveChanges();
                Response.Cookies.Append("Session", u.Session.ToString(), new CookieOptions {
                    SameSite = SameSiteMode.Lax
                });
                return Json(true);
                //return Redirect("/App");
            }
            return Json(false);
            //return BadRequest();
        }
    }
}
