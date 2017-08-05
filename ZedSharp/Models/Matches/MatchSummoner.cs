using Newtonsoft.Json;
using ZedSharp.Models.Enums;
using ZedSharp.Serializer.Converter;

namespace ZedSharp.Models.Matches
{
    public class MatchSummoner
    {
        public string SummonerName { get; set; }
        public int SummonerId { get; set; }
        public int AccountId { get; set; }
        public int ProfileIcon { get; set; }
        public int CurrentAccountId { get; set; }
        [JsonProperty(PropertyName = "PlatformId")]
        [JsonConverter(typeof(EnumToEnumConverter), typeof(PlatformId))]
        public Region Region { get; set; }
        [JsonProperty(PropertyName = "CurrentPlatformId")]
        [JsonConverter(typeof(EnumToEnumConverter), typeof(PlatformId))]
        public Region CurrentRegion { get; set; }
        public string MatchHistoryUri { get; set; }
    }
}