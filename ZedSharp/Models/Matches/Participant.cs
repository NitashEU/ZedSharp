using System.Collections.Generic;
using ZedSharp.Models.Enums;
using ZedSharp.Models.General;

namespace ZedSharp.Models.Matches
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public int ChampionId { get; set; }
        public MatchSummoner Summoner { get; set; }
        public int Spell1Id { get; set; }
        public int Spell2Id { get; set; }
        public Tier HighestSeasonLeague { get; set; }
        public ParticipantStats Stats { get; set; }
        public ParticipantTimeline Timeline { get; set; }
        public List<Rune> Runes { get; set; }
        public List<Mastery> Masteries { get; set; }
    }
}