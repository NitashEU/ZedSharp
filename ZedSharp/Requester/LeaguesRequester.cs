using System.Collections.Generic;
using System.Threading.Tasks;
using ZedSharp.Endpoints;
using ZedSharp.Models.Leagues;
using ZedSharp.Utils;

namespace ZedSharp.Requester
{
    public class LeaguesRequester
    {
        private readonly EndpointsHolder _endpointsHolder;
        private readonly RiotRequester _riotRequester;

        internal LeaguesRequester(EndpointsHolder endpointsHolder, RiotRequester riotRequester)
        {
            _endpointsHolder = endpointsHolder;
            _riotRequester = riotRequester;
        }

        public async Task<List<LeagueList>> GetLeaguesBySummonerIdAsync(long summonerId)
        {
            return await _riotRequester.GetAsync<List<LeagueList>>(_endpointsHolder.Leagues.BySummonerId, new
            {
                summonerId
            }.ToDictionary());
        }
    }
}