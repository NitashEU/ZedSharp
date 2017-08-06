using ZedSharp.Endpoints;
using ZedSharp.Models.Enums;
using ZedSharp.Requester;

namespace ZedSharp
{
    public class ZedClient
    {
        private readonly EndpointsHolder _endpointsHolder;
        private readonly RiotRequester _riotRequester;
        public Region Region { get; }

        public ZedClient(Region region, string apiKey, int maxTrys = 1, int timeout = 10000)
        {
            Region = region;

            var platformId = (PlatformId) (int) region;
            _endpointsHolder = new EndpointsHolder(platformId);
            _riotRequester = new RiotRequester(_endpointsHolder.BaseUrl, apiKey, maxTrys, timeout);
        }
        
        public LeaguesRequester Leagues => new LeaguesRequester(_endpointsHolder, _riotRequester);
        public MasterLeaguesRequester MasterLeagues => new MasterLeaguesRequester(_endpointsHolder, _riotRequester);
        public ChallengerLeaguesRequester ChallengerLeagues => new ChallengerLeaguesRequester(_endpointsHolder, _riotRequester);
        public SummonersRequester Summoners => new SummonersRequester(_endpointsHolder, _riotRequester);
        public MatchesRequester Matches => new MatchesRequester(_endpointsHolder, _riotRequester);
        public MatchlistsRequester Matchlists => new MatchlistsRequester(_endpointsHolder, _riotRequester);
        public StaticDataRequester StaticData => new StaticDataRequester(_endpointsHolder, _riotRequester);
    }
}
