using Codr.Data.Queries;
using Codr.Models;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codr.Data {
    public class UserProvider : AbstractUserProvider, IDisposable {
        public IDocumentSession Session { get; private set; }
        public UserProvider(IDocumentSession session) {
            Session = session;
        }

        public override User? GetUser(string id) {
            return Session.Load<User?>(id);
        }

        public override Dictionary<string, User?> GetUsers(IEnumerable<string> ids) {
            return Session.Load<User?>(ids);
        }

        public override User? GetUserByEmail(string email) {
            return Session.Query<Users_Emails.Result, Users_Emails>()
                .Where(u => u.Email == email)
                .OfType<User>()
                .FirstOrDefault();
        }

        public void Dispose() {
            Session.Dispose();
        }
    }
}
