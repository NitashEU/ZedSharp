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

        internal MatchesRequester(EndpointsHolder endpointsHolder, RiotRequester riotRequester)
        {
            _endpointsHolder = endpointsHolder;
            _riotRequester = riotRequester;
        }

        public async Task<Match> ByMatchIdAsync(long matchId)
        {
            return await _riotRequester.GetAsync<Match>(_endpointsHolder.Matches.ByMatchId, new
            {
                matchId
            }.ToDictionary());
        }
    }
}