namespace ZedSharp.Endpoints
{
    internal class LeaguesEndpoint
    {
        private readonly string _baseUrl = "lol/league/v3/leagues/";

        internal string BySummonerId => _baseUrl + "by-summoner/{summonerId}";
    }
}