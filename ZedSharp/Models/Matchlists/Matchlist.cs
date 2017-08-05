using System.Collections.Generic;

namespace ZedSharp.Models.Matchlists
{
    public class Matchlist
    {
        public int TotalGames { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public List<MatchReference> Matches { get; set; }
    }
}
