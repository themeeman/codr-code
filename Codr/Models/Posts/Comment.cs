using System;
using System.Collections.Generic;

namespace Codr.Models.Posts {
    public class Comment : IEquatable<Comment> {
        public Comment(string author, string content) {
            Author = author;
            Content = content;
        }

        public string Id { get; private set; } = string.Empty;
        public string Author { get; private set; }
        public string Content { get; private set; }
        public DateTime Created { get; private set; } = DateTime.Now;
        public HashSet<string> LikedBy { get; private set; } = new HashSet<string>();
        public HashSet<string> Comments { get; private set; } = new HashSet<string>();
        public int Likes => LikedBy.Count;

        public override bool Equals(object? obj) {
            if (obj is null || !(obj is Comment)) return false;
            return Equals((Comment)obj);
        }

        public bool Equals(Comment other) {
            return Author == other.Author &&
                   Created == other.Created;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Author, Created);
        }

        public static bool operator ==(Comment left, Comment right) {
            return left.Equals(right);
        }

        public static bool operator !=(Comment left, Comment right) {
            return !(left == right);
        }
    }
}
