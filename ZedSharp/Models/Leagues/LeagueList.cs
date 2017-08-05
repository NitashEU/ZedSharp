using System.Collections.Generic;
using ZedSharp.Models.Enums;

namespace ZedSharp.Models.Leagues
{
    public class LeagueList
    {
        public string Name { get; set; }
        public Tier Tier { get; set; }
        public Queue Queue { get; set; }
        public List<LeagueEntry> Entries { get; set; }
    }
}
