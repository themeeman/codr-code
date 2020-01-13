using Codr.Data;
using Codr.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Linq;

namespace Codr.Controllers {
    public class ApiController : Controller {

        [HttpGet]
        public IActionResult GetReplies(string id) {
            using var session = new UserProvider(DocumentStoreHolder.Store.OpenSession());
            var post = session.Session.Load<Post?>(id);
            var options = new JsonSerializerSettings {
                Converters = new List<JsonConverter> { new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" } }
            };
            if (post is { } p) {
                return Json(p.Comments.Select(s => session.Session.Load<Comment?>(s)).OrderBy(c => c?.Likes ?? 0), options);
            }
            var comment = session.Session.Load<Comment?>(id);
            if (comment is { } c) {
                return Json(c.Replies.Select(s => session.Session.Load<Comment?>(s)).OrderBy(c => c?.Likes ?? 0), options);
            }
            return Json(null);
        }

        [HttpGet]
        public IActionResult GetName(string id) {
            using var session = new UserProvider(DocumentStoreHolder.Store.OpenSession());
            var options = new JsonSerializerSettings {
                Converters = new List<JsonConverter> { new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" } }
            };
            return Json(session.GetUser(id)?.FullName, options);
        }
    }
}
