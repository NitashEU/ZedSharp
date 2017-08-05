using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZedSharp.Models.Matches;

namespace ZedSharp.Serializer.Converter
{
    public class TeamsConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var matchTeams = new List<MatchTeam>();

            var jobj = JToken.Load(reader);
            var root = (reader as JTokenReader)?.CurrentToken.Root;

            if (root == null)
            {
                return null;
            }

            matchTeams.Add(jobj.FirstOrDefault(j => j["win"].ToString() == "Win").ToObject<MatchTeam>());
            matchTeams.Add(jobj.FirstOrDefault(j => j["win"].ToString() == "Fail").ToObject<MatchTeam>());

            foreach (var mt in matchTeams)
            {
                mt.Participants = root["participants"].Where(j => j["teamId"].ToObject<int>() == mt.TeamId).Select(j => j.ToObject<Participant>()).ToList();
                foreach (var pt in mt.Participants)
                {
                    pt.Summoner = root["participantIdentities"].First(j => j["participantId"].ToObject<int>() == pt.ParticipantId)["player"].ToObject<MatchSummoner>();
                }
            }

            return matchTeams;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}