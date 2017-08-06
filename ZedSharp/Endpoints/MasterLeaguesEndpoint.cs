namespace ZedSharp.Endpoints
{
    internal class MasterLeaguesEndpoint
    {
        private readonly string _baseUrl = "lol/league/v3/masterleagues/";

        internal string ByQueue => _baseUrl + "by-queue/{queue}";
    }
}