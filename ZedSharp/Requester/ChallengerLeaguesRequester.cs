using System.Threading.Tasks;
using ZedSharp.Endpoints;
using ZedSharp.Models.Enums;
using ZedSharp.Models.Leagues;
using ZedSharp.Utils;

namespace ZedSharp.Requester
{
    public class ChallengerLeaguesRequester
    {
        private readonly EndpointsHolder _endpointsHolder;
        private readonly RiotRequester _riotRequester;

        public ChallengerLeaguesRequester(EndpointsHolder endpointsHolder, RiotRequester riotRequester)
        {
            _endpointsHolder = endpointsHolder;
            _riotRequester = riotRequester;
        }

        public async Task<LeagueList> GetChallengerLeaguesByQueue(Queue queue)
        {
            return await _riotRequester.Get<LeagueList>(_endpointsHolder.ChallengerLeagues.ByQueue, new
            {
                queue
            }.ToDictionary());
        }
    }
}