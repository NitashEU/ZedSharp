namespace ZedSharp.Endpoints
{
    public class SummonersEndpoint
    {
        private readonly string _baseUrl = "lol/summoner/v3/summoners/";

        public string ByAccountId => _baseUrl + "by-account/{accountId}";
        public string BySummonerName => _baseUrl + "by-name/{summonerName}";
        public string BySummonerId => _baseUrl + "{summonerId}";
    }
}