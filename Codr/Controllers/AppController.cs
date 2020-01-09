using Codr.Data;
using Codr.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Codr.Controllers {
    public class AppController : Controller {
        private User? ThisUser {
            get {
                using var session = new UserProvider(DocumentStoreHolder.Store.OpenSession());
                if (Request.Cookies.TryGetValue("Session", out string id)) {
                    Thread.Sleep(500); // what 
                    return session.GetUserBySessionId(id);
                }
                return null;
            }
        }

        public IActionResult Index() {
            if (ThisUser is { } u) {
                return View(u);
            }
            Response.Cookies.Delete("Session");
            return Redirect("/");
        }

        public IActionResult Friends(int page = 0) {
            if (ThisUser is { } u) {
                int maxPage = ((u.Friends.Count - 1) / 10) + 1;
                if (u.Friends.Count != 0 && page > maxPage)
                    page = maxPage;

                ViewData["Page"] = page;
                return View(u);
            }
            Response.Cookies.Delete("Session");
            return Redirect("/");
        }

        public IActionResult Profile(string id) {
            if (ThisUser is { } u) {
                if (string.IsNullOrEmpty(id)) {
                    return View(u);
                }
                using var session = new UserProvider(DocumentStoreHolder.Store.OpenSession());
                var user = session.GetUser(id);
                return View(user);
            }
            Response.Cookies.Delete("Session");
            return Redirect("/");
        }
    }
}
