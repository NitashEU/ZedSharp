using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZedSharp.Models.Enums;
using ZedSharp.Serializer.Converter;

namespace ZedSharp.Models.Matches
{
    public class Match
    {
        public long GameId { get; set; }
        public Season Season { get; set; }
        public Queue Queue { get; set; }
        [JsonProperty(PropertyName = "PlatformId")]
        [JsonConverter(typeof(EnumToEnumConverter), typeof(PlatformId))]
        public Region Region { get; set; }
        public string GameVersion { get; set; }
        public string GameMode { get; set; }
        public string GameType { get; set; }
        public LeagueMap Map { get; set; }
        public int GameDuration { get; set; }
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime PlayedOn { get; set; }
        [JsonProperty(PropertyName = "Teams")]
        [JsonConverter(typeof(TeamsConverter))]
        public List<MatchTeam> MatchTeams { get; set; }
    }
}
