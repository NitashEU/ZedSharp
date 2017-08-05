using Newtonsoft.Json;
using ZedSharp.Serializer.Converter;

namespace ZedSharp.Models.Leagues
{
    public class LeagueEntry
    {
        [JsonProperty(PropertyName = "Rank")]
        [JsonConverter(typeof(RankConverter))]
        public int Division { get; set; }
        public bool HotStreak { get; set; }
        public MiniSeries MiniSeries { get; set; }
        public int Wins { get; set; }
        public bool Veteran { get; set; }
        public int Losses { get; set; }
        public string PlayerOrTeamId { get; set; }
        public string PlayerOrTeamName { get; set; }
        public bool Inactive { get; set; }
        public bool FreshBlood { get; set; }
        public int LeaguePoints { get; set; }
    }
}