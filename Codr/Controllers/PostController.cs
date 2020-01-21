using Codr.Data;
using Codr.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        public IActionResult Render(string components) {
            var post = new Post();
            var _components = JsonConvert.DeserializeObject<List<IPostComponent>>(components, new ComponentSerializer());
            post.Components.AddRange(_components);
            return View(post);
        }
    }
}
