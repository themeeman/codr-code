using Codr.Data;
using Codr.Models;
using Codr.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Codr.Controllers {
    public class AppController : Controller {
        private readonly UserProvider session = new UserProvider(DocumentStoreHolder.Store.OpenSession());
        private User? ThisUser {
            get {
                if (Request.Cookies.TryGetValue("Session", out string id)) {
                    Thread.Sleep(1000); // what 
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

        public IActionResult Friends() {
            if (ThisUser is { } u) { 
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
                var user = session.GetUser(id);
                ViewData["LoggedIn"] = u.Id;
                return View(user);
            }
            Response.Cookies.Delete("Session");
            return Redirect("/");
        }

        [Route("App/Profile/AddComment")]
        [HttpGet]
        public IActionResult AddComment() {
            return Redirect("/App");
        }

        private struct Result {
            public HashSet<string> Comments { get; private set; }
        }

        [Route("App/Profile/AddComment")]
        [HttpPost]
        public IActionResult AddComment(string postId, string content) {
            if (ThisUser is { } u) {
                var post = session.Session.Load<Result?>(postId);
                if (post is { } p) {
                    var comment = new Comment(u.Id, content);
                    session.Session.Store(comment);
                    session.Session.SaveChanges();
                    p.Comments.Add(session.Session.Advanced.GetDocumentId(comment));
                    session.Session.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [Route("App/Profile/NewPost")]
        [HttpGet]
        public IActionResult NewPost() {
            if (ThisUser is { } u) {
                return View(u);
            }
            Response.Cookies.Delete("Session");
            return Redirect("/");
        }

        [Route("App/Profile/NewPost")]
        [HttpPost]
        public IActionResult NewPost(string components) {
            if (ThisUser is { } u) {
                var post = new Post(u.Id);
                var _components = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IPostComponent>>(Uri.UnescapeDataString(components), new ComponentSerializer());
                post.Components.AddRange(_components);
                session.Session.Store(post);
                session.Session.SaveChanges();
                ThisUser.Posts.Add(session.Session.Advanced.GetDocumentId(post));
                session.Session.SaveChanges();
            }
            
            return Redirect("/");
        }

        public IActionResult Search(string query = "") {
            if (ThisUser is { } u) {
                ViewData["Query"] = query;
                return View(u);
            }
            return Redirect("/");
        }

        public IActionResult Logout() {
            Response.Cookies.Delete("Session");
            if (ThisUser is { } u) {
                u.Session = new Guid();
                session.Session.SaveChanges();
            }
            return Redirect("/");
        }
    }
}
