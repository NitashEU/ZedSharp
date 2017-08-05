using System.Threading.Tasks;
using ZedSharp.Endpoints;
using ZedSharp.Models.Summoners;
using ZedSharp.Utils;

namespace ZedSharp.Requester
{
    public class SummonersRequester
    {
        private readonly EndpointsHolder _endpointsHolder;
        private readonly RiotRequester _riotRequester;

        public SummonersRequester(EndpointsHolder endpointsHolder, RiotRequester riotRequester)
        {
            _endpointsHolder = endpointsHolder;
            _riotRequester = riotRequester;
        }

        public async Task<Summoner> GetSummonerByAccountId(long accountId)
        {
            return await _riotRequester.Get<Summoner>(_endpointsHolder.Summoners.ByAccountId, new
            {
                accountId
            }.ToDictionary());
        }

        public async Task<Summoner> GetSummonerBySummonerName(string summonerName)
        {
            return await _riotRequester.Get<Summoner>(_endpointsHolder.Summoners.BySummonerName, new
            {
                summonerName
            }.ToDictionary());
        }

        public async Task<Summoner> GetSummonerBySummonerId(long summonerId)
        {
            return await _riotRequester.Get<Summoner>(_endpointsHolder.Summoners.BySummonerId, new
            {
                summonerId
            }.ToDictionary());
        }
    }
}