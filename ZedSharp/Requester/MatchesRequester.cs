using System.Threading.Tasks;
using ZedSharp.Endpoints;
using ZedSharp.Models.Matches;
using ZedSharp.Utils;

namespace ZedSharp.Requester
{
    public class MatchesRequester
    {
        private readonly EndpointsHolder _endpointsHolder;
        private readonly RiotRequester _riotRequester;

        public MatchesRequester(EndpointsHolder endpointsHolder, RiotRequester riotRequester)
        {
            _endpointsHolder = endpointsHolder;
            _riotRequester = riotRequester;
        }

        public async Task<Match> GetMatchByMatchId(long matchId)
        {
            return await _riotRequester.Get<Match>(_endpointsHolder.Matches.ByMatchId, new
            {
                matchId
            }.ToDictionary());
        }
    }
}