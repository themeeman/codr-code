using System;

namespace Codr.Models.Posts {
    [Flags]
    public enum TextStyle {
        None = 0x00,
        Bold = 0x01,
        Underlined = 0x02,
        Italics = 0x04,
        Strikethrough = 0x08,
        InlineCode = 0x10,
        Spoiler = 0x20,
    }

    public static class TextStyleMethods {
        public static bool HasStyle(this TextStyle self, TextStyle other) {
            return (self & other) != TextStyle.None;
        }
    }
}
