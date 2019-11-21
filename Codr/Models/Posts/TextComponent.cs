using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Codr.Models.Posts {
    public class TextComponent : IEquatable<TextComponent> {
        public TextComponent() { }
        public TextComponent(string content, TextStyle style) {
            Content = content;
            Style = style;
        }

        public string Content { get; private set; } = string.Empty;
        public TextStyle Style { get; private set; } = TextStyle.None;

        public override bool Equals(object? obj) {
            if (obj is null) return false;
            return Equals((TextComponent)obj);
        }

        public bool Equals(TextComponent other) {
            return Content == other.Content &&
                   Style == other.Style;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Content, Style);
        }

        public static bool operator ==(TextComponent left, TextComponent right) {
            return left.Equals(right);
        }

        public static bool operator !=(TextComponent left, TextComponent right) {
            return !(left == right);
        }
    }
}
