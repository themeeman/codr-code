using Codr.Models;
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
}
