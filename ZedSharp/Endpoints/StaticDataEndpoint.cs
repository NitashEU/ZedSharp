namespace ZedSharp.Endpoints
{
    internal class StaticDataEndpoint
    {
        private readonly string _baseUrl = "lol/static-data/v3/";

        internal string Versions => _baseUrl + "versions";
    }
}