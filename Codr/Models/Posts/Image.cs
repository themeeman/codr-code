using System;

namespace Codr.Models.Posts {
    public class Image : IPostComponent, IEquatable<Image> {
        public Image(string url) {
            Url = url;
        }

        public string Url { get; private set; }

        public override bool Equals(object? obj) {
            if (obj is null || !(obj is Image)) return false;
            return Equals((Image)obj);
        }

        public bool Equals(Image other) {
            return Url == other.Url;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Url);
        }

        public static bool operator ==(Image left, Image right) {
            return left.Equals(right);
        }

        public static bool operator !=(Image left, Image right) {
            return !(left == right);
        }
    }
}
