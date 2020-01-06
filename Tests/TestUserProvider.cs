using Codr;
using Codr.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tests {
    class TestUserProvider : AbstractUserProvider {
        private readonly Dictionary<string, User> users = new Dictionary<string, User> {
            { "1", new User("1", new HashedPassword("1"), "1") },
            { "2", new User("2", new HashedPassword("2"), "2") },
            { "3", new User("3", new HashedPassword("3"), "3") },
            { "4", new User("4", new HashedPassword("4"), "4") },
            { "5", new User("5", new HashedPassword("5"), "5") }
        };

        public TestUserProvider() {
            GetUser("1")!.AddFriend(GetUser("2")!);
            GetUser("2")!.AddFriend(GetUser("3")!);
            GetUser("3")!.AddFriend(GetUser("4")!);
            GetUser("4")!.AddFriend(GetUser("5")!);
        }

        public override User? GetUser(string id) {
            try {
                return users[id];
            } catch {
                return null;
            }
        }

        public override Dictionary<string, User?> GetUsers(IEnumerable<string> ids) {
            return new Dictionary<string, User?>(
                ids.Select(s => KeyValuePair.Create(s, GetUser(s)))
            );
        }
    }
}
