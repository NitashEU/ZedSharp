using System.Collections.Generic;
using Newtonsoft.Json;
using ZedSharp.Serializer.Converter;

namespace ZedSharp.Models.Matches
{
    public class MatchTeam
    {
        public int TeamId { get; set; }
        [JsonProperty(PropertyName = "Win")]
        [JsonConverter(typeof(RiotWinBoolConverter))]
        public bool Won { get; set; }
        public List<MatchBan> Bans { get; set; }
        public bool FirstDragon { get; set; }
        public bool FirstInhibitor { get; set; }
        public bool FirstRiftHerald { get; set; }
        public bool FirstBaron { get; set; }
        public bool FirstBlood { get; set; }
        public bool FirstTower { get; set; }
        public int BaronKills { get; set; }
        public int RiftHeraldKills { get; set; }
        public int VilemawKills { get; set; }
        public int InhibitorKills { get; set; }
        public int TowerKills { get; set; }
        public int DragonKills { get; set; }
        public int DominionVictoryScore { get; set; }
        public List<Participant> Participants { get; set; }
    }
}