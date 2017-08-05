namespace ZedSharp.Endpoints
{
    public class MasterLeaguesEndpoint
    {
        private readonly string _baseUrl = "lol/league/v3/masterleagues/";

        public string ByQueue => _baseUrl + "by-queue/{queue}";
    }
}