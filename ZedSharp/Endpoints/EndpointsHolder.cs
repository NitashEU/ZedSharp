using ZedSharp.Models.Enums;

namespace ZedSharp.Endpoints
{
    public class EndpointsHolder
    {
        public readonly string BaseUrl;

        public EndpointsHolder(PlatformId platformId)
        {
            BaseUrl = $"https://{platformId}.api.riotgames.com";
        }

        public LeaguesEndpoint Leagues = new LeaguesEndpoint();
        public MasterLeaguesEndpoint MasterLeagues = new MasterLeaguesEndpoint();
        public ChallengerLeaguesEndpoint ChallengerLeagues = new ChallengerLeaguesEndpoint();
        public SummonersEndpoint Summoners = new SummonersEndpoint();
        public MatchesEndpoint Matches = new MatchesEndpoint();
        public MatchlistsEndpoint Matchlists = new MatchlistsEndpoint();
        public StaticDataEndpoint StaticData = new StaticDataEndpoint();
    }
}
