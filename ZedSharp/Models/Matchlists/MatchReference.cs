using System;
using Newtonsoft.Json;
using ZedSharp.Serializer.Converter;

namespace ZedSharp.Models.Matchlists
{
    public class MatchReference
    {
        public long GameId { get; set; }
        public string PlatformId { get; set; }
        public int Season { get; set; }
        public int Queue { get; set; }
        public int Champion { get; set; }
        public string Role { get; set; }
        public string Lane { get; set; }
        [JsonProperty(PropertyName = "Timestamp")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime PlayedOn { get; set; }
    }
}