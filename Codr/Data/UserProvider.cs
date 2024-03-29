﻿using Codr.Data.Queries;
using Codr.Models;
using Codr.Models.Posts;
using Raven.Client.Documents;
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
         
        public override User? GetUserBySessionId(string id) {
            return Session.Query<Users_Session.Result, Users_Session>()
                .Where(u => u.Session.ToString() == id)
                .OfType<User>()
                .FirstOrDefault();
        }

        public List<User> Search(string fullName) {
            return Session.Query<Users_FullName.Result, Users_FullName>()
                .Search(u => u.FullName, $"*{fullName}*")
                .OfType<User>()
                .ToList();
        }

        public List<Post> RecentPosts(IEnumerable<string> ids, int number) {
            return ids.Select(id => Session.Query<Posts_Created.Result, Posts_Created>()
                    .Where(p => p.Author == id)
                    .OrderByDescending(p => p.Created)
                    .OfType<Post>())
                .SelectMany(i => i)
                .Take(number)
                .ToList();
                
        }

        public void Dispose() {
            Session.Dispose();
        }
    }
}
