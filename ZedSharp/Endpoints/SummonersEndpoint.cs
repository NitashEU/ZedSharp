namespace ZedSharp.Endpoints
{
    internal class SummonersEndpoint
    {
        private readonly string _baseUrl = "lol/summoner/v3/summoners/";

        internal string ByAccountId => _baseUrl + "by-account/{accountId}";
        internal string BySummonerName => _baseUrl + "by-name/{summonerName}";
        internal string BySummonerId => _baseUrl + "{summonerId}";
    }
}