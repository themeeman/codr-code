using Codr.Data;
using Microsoft.AspNetCore.Mvc;

namespace Codr.Controllers {
    public class AppController : Controller {
        public IActionResult Index() {
            using var session = new UserProvider(DocumentStoreHolder.Store.OpenSession());
            if (Request.Cookies.TryGetValue("Session", out string id)) {
                if (session.GetUserBySessionId(id) is { } user) {
                    return View(user);
                } else {
                    Response.Cookies.Delete("Session");
                }
            }
            return Redirect("/");
        }
    }
}
