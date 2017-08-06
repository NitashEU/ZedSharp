using System.Threading.Tasks;
using ZedSharp.Endpoints;
using ZedSharp.Models.Enums;
using ZedSharp.Models.Leagues;
using ZedSharp.Utils;

namespace ZedSharp.Requester
{
    public class MasterLeaguesRequester
    {
        private readonly EndpointsHolder _endpointsHolder;
        private readonly RiotRequester _riotRequester;

        internal MasterLeaguesRequester(EndpointsHolder endpointsHolder, RiotRequester riotRequester)
        {
            _endpointsHolder = endpointsHolder;
            _riotRequester = riotRequester;
        }

        public async Task<LeagueList> ByQueueAsync(Queue queue)
        {
            return await _riotRequester.GetAsync<LeagueList>(_endpointsHolder.MasterLeagues.ByQueue, new
            {
                queue
            }.ToDictionary());
        }
    }
}