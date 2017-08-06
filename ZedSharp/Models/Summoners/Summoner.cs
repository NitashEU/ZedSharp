using System;
using Newtonsoft.Json;
using ZedSharp.Serializer.Converter;

namespace ZedSharp.Models.Summoners
{
    public class Summoner
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public int SummonerLevel { get; set; }
        public int ProfileIconId { get; set; }
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime RevisionDate { get; set; }
    }
}
