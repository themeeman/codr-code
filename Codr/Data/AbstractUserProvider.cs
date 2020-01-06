using System.Collections.Generic;
using Codr.Models;

namespace Codr.Data {
    public abstract class AbstractUserProvider {
        public abstract User? GetUser(string id);
        public abstract Dictionary<string, User?> GetUsers(IEnumerable<string> ids);
        public abstract User? GetUserByEmail(string email);
        public abstract User? GetUserBySessionId(string id);
        public int? FriendDistance(User start, User end) {
            if (start == end) {
                return 0;
            }
            var distance = 1;
            var visited = new HashSet<string> { start.Id };
            while (true) {
                var current = new HashSet<string>();
                foreach (var visitedUser in GetUsers(visited)) {
                    if (visitedUser.Value is { } v)
                        current.UnionWith(v.Friends);
                }
                if (current.Count == 0)
                    return null;
                else if (current.Contains(end.Id))
                    return distance;

                distance++;
                visited.UnionWith(current);
            }
        }

        public double FriendQuotient(User left, User right) {
            if (FriendDistance(left, right) is { } d)
                return 1.0 / (d + 1);
            else
                return 0.0;
        }
    }
}
