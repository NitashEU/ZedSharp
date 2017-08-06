namespace ZedSharp.Endpoints
{
    internal class MatchesEndpoint
    {
        private readonly string _baseUrl = "lol/match/v3/matches/";

        internal string ByMatchId => _baseUrl + "{matchId}";
    }
}