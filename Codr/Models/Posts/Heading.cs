using System;

namespace Codr.Models.Posts {
    public class Heading : IPostComponent, IEquatable<Heading> {
        public Heading(string content, byte size) {
            Content = content;
        }

        public string Content { get; private set; } = string.Empty;

        public override bool Equals(object? obj) {
            if (obj is null) return false;
            return Equals((Heading)obj);
        }

        public bool Equals(Heading other) {
            return Content == other.Content;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Content);
        }

        public static bool operator ==(Heading left, Heading right) {
            return left.Equals(right);
        }

        public static bool operator !=(Heading left, Heading right) {
            return !(left == right);
        }
    }
}
