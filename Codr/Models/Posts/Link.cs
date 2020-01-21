using System;

namespace Codr.Models.Posts {
    public class Link : IPostComponent, IEquatable<Link> {
        public Link(string url, string? content = null) {
            Content = content;
            Url = url;
        }

        public string? Content { get; private set; }
        public string Url { get; private set; }

        public override bool Equals(object? obj) {
            if (obj is null || !(obj is Link)) return false;
            return Equals((Link)obj);
        }

        public bool Equals(Link other) {
            return Content == other.Content &&
                   Url == other.Url;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Content, Url);
        }

        public static bool operator ==(Link left, Link right) {
            return left.Equals(right);
        }

        public static bool operator !=(Link left, Link right) {
            return !(left == right);
        }
    }
}
