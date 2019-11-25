using System;

namespace Codr.Models.Posts {
    [Flags]
    public enum TextStyle {
        None          = 0x00, // 0b000000
        Bold          = 0x01, // 0b000001
        Underlined    = 0x02, // 0b000010
        Italics       = 0x04, // 0b000100
        Strikethrough = 0x08, // 0b001000
        InlineCode    = 0x10, // 0b010000
        Spoiler       = 0x20, // 0b100000
    }
}
