namespace ZedSharp.Endpoints
{
    public class LeaguesEndpoint
    {
        private readonly string _baseUrl = "lol/league/v3/leagues/";

        public string BySummonerId => _baseUrl + "by-summoner/{summonerId}";
    }
}