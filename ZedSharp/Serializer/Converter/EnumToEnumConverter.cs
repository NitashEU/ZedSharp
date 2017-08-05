using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZedSharp.Serializer.Converter
{
    public class EnumToEnumConverter : JsonConverter
    {
        private readonly Type _baseType;

        public EnumToEnumConverter(Type baseType)
        {
            _baseType = baseType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var baseEnum = Enum.Parse(_baseType, JToken.Load(reader).Value<string>());
            return Enum.ToObject(objectType, (int) baseEnum);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}