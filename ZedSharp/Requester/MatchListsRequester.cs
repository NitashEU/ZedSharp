using System.Threading.Tasks;
using ZedSharp.Endpoints;
using ZedSharp.Models.Matchlists;
using ZedSharp.RequestOptions;
using ZedSharp.Utils;

namespace ZedSharp.Requester
{
    public class MatchlistsRequester
    {
        private readonly EndpointsHolder _endpointsHolder;
        private readonly RiotRequester _riotRequester;

        internal MatchlistsRequester(EndpointsHolder endpointsHolder, RiotRequester riotRequester)
        {
            _endpointsHolder = endpointsHolder;
            _riotRequester = riotRequester;
        }

        public async Task<Matchlist> ByAccountIdAsync(long accountId, MatchlistByAccountIdOptions requestOptions = null)
        {
            return await _riotRequester.GetAsync<Matchlist>(_endpointsHolder.Matchlists.ByAccountId, new
            {
                accountId
            }.ToDictionary(), requestOptions ?? new MatchlistByAccountIdOptions());
        }
    }
}