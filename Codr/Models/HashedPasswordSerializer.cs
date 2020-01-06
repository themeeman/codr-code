using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Codr.Models {
    public class HashedPasswordSerializer : JsonConverter<HashedPassword> {
        public override HashedPassword ReadJson(JsonReader reader, Type objectType, HashedPassword existingValue, bool hasExistingValue, JsonSerializer serializer) {
            var obj = (HashedPassword)FormatterServices.GetUninitializedObject(typeof(HashedPassword));
            var s = reader.Value as string;
            obj.GetType().GetProperty("Password")!.SetValue(obj, s);
            return obj;
        }

        public override void WriteJson(JsonWriter writer, HashedPassword value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString());
        }
    }
}
