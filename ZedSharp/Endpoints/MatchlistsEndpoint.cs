namespace ZedSharp.Endpoints
{
    internal class MatchlistsEndpoint
    {
        private readonly string _baseUrl = "lol/match/v3/matchlists/";

        internal string ByAccountId => _baseUrl + "by-account/{accountId}";
        internal string ByAccountIdRecent => _baseUrl + "by-account/{accountId}/recent";
    }
}