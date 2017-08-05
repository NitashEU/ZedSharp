using System;
using Newtonsoft.Json;
using ZedSharp.Serializer.Converter;

namespace ZedSharp.Models.Summoners
{
    public class Summoner
    {
        public int Id { get; set; }
        public int Accountid { get; set; }
        public string Name { get; set; }
        public int Summonerlevel { get; set; }
        public int Profileiconid { get; set; }
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime Revisiondate { get; set; }
    }
}
