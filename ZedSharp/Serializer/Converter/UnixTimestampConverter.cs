using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZedSharp.Serializer.Converter
{
    public class UnixTimestampConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobj = JToken.Load(reader);
            if (jobj.Type == JTokenType.Integer)
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(jobj.Value<long>()).DateTime;
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }
    }
}
