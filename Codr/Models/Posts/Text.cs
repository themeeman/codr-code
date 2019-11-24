using System;
using System.Collections.Generic;
using System.Linq;

namespace Codr.Models.Posts {
    public class Text : IPostComponent, IEquatable<Text> {
        public Text() { }
        public Text(List<TextComponent> components) {
            Components = components;
        }

        public List<TextComponent> Components { get; private set; } = new List<TextComponent>();

        public override bool Equals(object? obj) {
            if (obj is null) return false;
            return Equals((Text)obj);
        }

        public bool Equals(Text other) {
            return Components.SequenceEqual(other.Components);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Components);
        }

        public static bool operator ==(Text left, Text right) {
            return left.Equals(right);
        }

        public static bool operator !=(Text left, Text right) {
            return !(left == right);
        }
    }
}
