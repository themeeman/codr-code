using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Codr.Models.Posts {
    public class ComponentSerializer : JsonConverter<IPostComponent> {
        public override IPostComponent ReadJson(JsonReader reader, Type objectType, IPostComponent existingValue, bool hasExistingValue, JsonSerializer serializer) {
            var o = JObject.Load(reader);
            string s;
            return (s = o["Type"].ToString()) switch {
                "code" => new Code(o["Code"].ToString(), o["Language"].ToString() == "Plaintext" ? (Language?)null : Enum.Parse<Language>(o["Language"].ToString())),
                "heading" => new Heading(o["Content"].ToString()),
                "image" => new Image(o["Url"].ToString()),
                "link" => new Link(o["Url"].ToString(), o["Content"].ToString()),
                "text" => new Text(new TextComponent[]{ new TextComponent(o["Content"].ToString()) }),
                _ => throw new ArgumentException()
            };
        }

        public override void WriteJson(JsonWriter writer, IPostComponent value, JsonSerializer serializer) {
            writer.WriteValue(value);
        }
    }
}
