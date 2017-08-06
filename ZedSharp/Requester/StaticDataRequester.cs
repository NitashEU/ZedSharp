using System.Collections.Generic;
using System.Threading.Tasks;
using ZedSharp.Endpoints;

namespace ZedSharp.Requester
{
    public class StaticDataRequester
    {
        private readonly EndpointsHolder _endpointsHolder;
        private readonly RiotRequester _riotRequester;

        internal StaticDataRequester(EndpointsHolder endpointsHolder, RiotRequester riotRequester)
        {
            _endpointsHolder = endpointsHolder;
            _riotRequester = riotRequester;
        }

        public async Task<List<string>> VersionsAsync(bool wait = false)
        {
            return await _riotRequester.GetAsync<List<string>>(_endpointsHolder.StaticData.Versions, null, null, wait);
        }
    }
}