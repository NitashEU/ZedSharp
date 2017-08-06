namespace ZedSharp.Endpoints
{
    internal class ChallengerLeaguesEndpoint
    {
        private readonly string _baseUrl = "lol/league/v3/challengerleagues/";

        internal string ByQueue => _baseUrl + "by-queue/{queue}";
    }
}