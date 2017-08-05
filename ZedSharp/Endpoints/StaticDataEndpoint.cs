namespace ZedSharp.Endpoints
{
    public class StaticDataEndpoint
    {
        private readonly string _baseUrl = "lol/static-data/v3/";

        public string Versions => _baseUrl + "versions";
    }
}