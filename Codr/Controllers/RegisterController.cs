using Codr.Data;
using Codr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codr.Controllers {
    public class RegisterController : Controller {
        class Result {
            public Result(string type, string reason) {
                Type = type;
                Reason = reason;
            }
            public string Type { get; set; } = string.Empty;
            public string Reason { get; set; } = string.Empty;
        }
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Process(string email, string password, string firstName, string? lastName) {
            using var session = new UserProvider(DocumentStoreHolder.Store.OpenSession());
            if (session.GetUserByEmail(email) is { })
                return BadRequest();

            session.Session.Store(new User(email, new HashedPassword(password), firstName, lastName));
            session.Session.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult Process() {
            return Redirect("/Register");
        }
    }
}
