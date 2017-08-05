namespace ZedSharp.Endpoints
{
    public class ChallengerLeaguesEndpoint
    {
        private readonly string _baseUrl = "lol/league/v3/challengerleagues/";

        public string ByQueue => _baseUrl + "by-queue/{queue}";
    }
}