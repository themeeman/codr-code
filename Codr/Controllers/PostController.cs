using Codr.Data;
using Codr.Models.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Codr.Controllers {
    public class PostController : Controller {
        public IActionResult Index(string id) {
            using var session = DocumentStoreHolder.Store.OpenSession();
            var post = session.Load<Post>(id);

            if (post is null) {
                return NotFound(id);
            } else {
                return View(post);
            }
        }
    }
}
