using System;
using System.Collections.Generic;
using System.Linq;
using ZedSharp.Models.Enums;
using ZedSharp.Utils;

namespace ZedSharp.RequestOptions
{
    public class MatchlistByAccountIdOptions : IRequestOptions
    {
        public List<Season> Seasons { get; set; }
        public List<Queue> Queues { get; set; }
        public List<int> Champions { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? BeginIndex { get; set; }
        public int? EndIndex { get; set; }

        public Dictionary<string, object> GetRiotOptions()
        {
            var dic = new Dictionary<string, object>();

            if (Seasons != null && Seasons.Count > 0)
            {
                dic.Add("season", Seasons.Select(s => (int)s));
            }
            if (Queues != null && Queues.Count > 0)
            {
                dic.Add("queue", Queues.Select(q => (int)q));
            }
            if (Champions != null && Champions.Count > 0)
            {
                dic.Add("champion", Champions);
            }
            if (BeginTime != default(DateTime))
            {
                dic.Add("beginTime", BeginTime.ToUnixTimeMilliseconds());
            }
            if (EndTime != default(DateTime))
            {
                dic.Add("endTime", EndTime.ToUnixTimeMilliseconds());
            }
            if (BeginIndex != null)
            {
                dic.Add("beginIndex", BeginIndex);
            }
            if (EndIndex != null)
            {
                dic.Add("endIndex", EndIndex);
            }

            return dic;
        }
}
}
