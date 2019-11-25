using System;

namespace Codr.Models.Posts {
    public class Code : IPostComponent, IEquatable<Code> {
        public Code() { }
        public Code(string content, Language? language) {
            Content = content;
            Language = language;
        }

        public string Content { get; private set; } = string.Empty;
        public Language? Language { get; private set; }

        public override bool Equals(object? obj) {
            if (obj is null || !(obj is Code)) return false;
            return Equals((Code)obj);
        }

        public bool Equals(Code other) {
            return Content == other.Content &&
                   Language == other.Language;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Content, Language);
        }

        public static bool operator ==(Code left, Code right) {
            return left.Equals(right);
        }

        public static bool operator !=(Code left, Code right) {
            return !(left == right);
        }
    }
}
