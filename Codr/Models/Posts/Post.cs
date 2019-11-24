using System;
using System.Collections.Generic;
using System.Linq;

namespace Codr.Models.Posts {
    public class Post : IEquatable<Post> {
        public Post() { }
        public Post(string author) {
            Author = author;
        }

        public string Id { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;
        public DateTime Created { get; private set; } = DateTime.Now;
        public HashSet<string> LikedBy { get; private set; } = new HashSet<string>();
        public List<IPostComponent> Components { get; private set; } = new List<IPostComponent>();
        public HashSet<string> Comments { get; private set; } = new HashSet<string>();
        public int Likes => LikedBy.Count();

        public override bool Equals(object? obj) {
            if (obj is null) return false;
            return Equals((Post)obj);
        }

        public bool Equals(Post other) {
            return Id == other.Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(Post left, Post right) {
            return left.Equals(right);
        }

        public static bool operator !=(Post left, Post right) {
            return !(left == right);
        }
    }
}
