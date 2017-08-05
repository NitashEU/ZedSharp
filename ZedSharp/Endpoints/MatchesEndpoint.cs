namespace ZedSharp.Endpoints
{
    public class MatchesEndpoint
    {
        private readonly string _baseUrl = "lol/match/v3/matches/";

        public string ByMatchId => _baseUrl + "{matchId}";
    }
}