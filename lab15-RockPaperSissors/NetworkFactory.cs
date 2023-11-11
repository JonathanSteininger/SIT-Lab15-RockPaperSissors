using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockPaperScissorsLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lab15_RockPaperSissors
{
    public static class NetworkFactory
    {
        public static string Serialize(object Object) => JsonConvert.SerializeObject(Object);

        public static T Deserialize<T>(string JsonObject) => JsonConvert.DeserializeObject<T>(JsonObject);

        public static object DeserializeAuto(string JsonObject)
        {
            JObject JO = JObject.Parse(JsonObject);
            string type = (string)JO["Type"];
            switch(type)
            {
                case nameof(PlayerInfo): return Deserialize<PlayerInfo>(JsonObject);
                case nameof(GameCmd): return Deserialize<GameCmd>(JsonObject);
                case nameof(GameMessage): return Deserialize<GameMessage>(JsonObject);
                default: return null;
            }
        }
    }
}
