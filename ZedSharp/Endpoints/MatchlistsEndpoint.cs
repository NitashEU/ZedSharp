namespace ZedSharp.Endpoints
{
    public class MatchlistsEndpoint
    {
        private readonly string _baseUrl = "lol/match/v3/matchlists/";

        public string ByAccountId => _baseUrl + "by-account/{accountId}";
        public string ByAccountIdRecent => _baseUrl + "by-account/{accountId}/recent";
    }
}