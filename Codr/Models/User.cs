using System;
using System.Collections.Generic;

namespace Codr.Models {
    public class User : IEquatable<User> {
        public string Id { get; private set; } = string.Empty;
        public string Email { get; private set; }
        public string Password {
            get => password;
            set {
                byte[] salt;

            }
        }

        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public List<string> Posts { get; private set; } = new List<string>();
        public IEnumerable<string> Friends => friends;
        private readonly HashSet<string> friends = new HashSet<string>();
        private string password = string.Empty;

        public User(string email, string password, string firstName, string? lastName = null) {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
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
