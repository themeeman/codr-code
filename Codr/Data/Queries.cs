using Codr.Models;
using Codr.Models.Posts;
using Raven.Client.Documents.Indexes;
using System;
using System.Linq;

namespace Codr.Data.Queries {
    class Users_Emails : AbstractIndexCreationTask<User> {
        public class Result {
            public string Email { get; set; } = string.Empty;
        }

        public Users_Emails() {
            Map = users => users.Select(user => new { user.Email });
        }
    }

    class Users_Session : AbstractIndexCreationTask<User> {
        public class Result {
            public Guid Session { get; set; }
        }

        public Users_Session() {
            Map = users => users.Select(user => new { user.Session });
        }
    }

    class Users_FullName : AbstractIndexCreationTask<User> { 
        public class Result {
            public string FullName { get; set; } = string.Empty;
        }

        public Users_FullName() {
            Map = users => users.Select(user => new { user.FullName });
        }
    }

    class Posts_Created : AbstractIndexCreationTask<Post> {
        public class Result {
            public string Author { get; set; } = string.Empty;
            public DateTime Created { get; set; }
        }

        public Posts_Created() {
            Map = posts => posts.Select(post => new { post.Author, post.Created });
        }
    }
}
