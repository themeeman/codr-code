using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Codr.Models {
    public class User : IEquatable<User> {
        public string Id { get; private set; } = string.Empty;
        public string Email { get; private set; }
        public HashedPassword Password { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid Session { get; set; }
        public string? ProfilePicture { get; set; }
        public List<string> Posts { get; private set; } = new List<string>();
        [JsonProperty]
        public IReadOnlyCollection<string> Friends {
            get => friends;
            private set => friends.UnionWith(value);
        }

        private readonly HashSet<string> friends = new HashSet<string>();

        public User(string email, HashedPassword password, string firstName, string? lastName = null) {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Session = Guid.NewGuid();
        }

        public void AddFriend(User other) {
            friends.Add(other.Id);
            other.friends.Add(Id);
        }

        public void NewSession() {
            Session = Guid.NewGuid();
        }

        public override bool Equals(object? obj) {
            if (obj is null || !(obj is User))
                return false;
            return Equals((User)obj);
        }

        public bool Equals(User other) {
            return Id == other.Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(User left, User right) {
            return left.Equals(right);
        }

        public static bool operator !=(User left, User right) {
            return !(left == right);
        }
    }
}
