using ZedSharp.Models.Enums;

namespace ZedSharp.Endpoints
{
    internal class EndpointsHolder
    {
        public readonly string BaseUrl;

        public EndpointsHolder(PlatformId platformId)
        {
            BaseUrl = $"https://{platformId}.api.riotgames.com";
        }

        internal LeaguesEndpoint Leagues = new LeaguesEndpoint();
        internal MasterLeaguesEndpoint MasterLeagues = new MasterLeaguesEndpoint();
        internal ChallengerLeaguesEndpoint ChallengerLeagues = new ChallengerLeaguesEndpoint();
        internal SummonersEndpoint Summoners = new SummonersEndpoint();
        internal MatchesEndpoint Matches = new MatchesEndpoint();
        internal MatchlistsEndpoint Matchlists = new MatchlistsEndpoint();
        internal StaticDataEndpoint StaticData = new StaticDataEndpoint();
    }
}
