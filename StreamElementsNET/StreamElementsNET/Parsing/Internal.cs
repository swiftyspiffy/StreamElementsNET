using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamElementsNET.Parsing
{
    internal static class Internal
    {
        public static Models.Internal.Authenticated handleAuthenticated(JArray json)
        {
            //["authenticated",{"clientId":"nmccwgA5DoIqh9ZYAAqx","channelId":"59b84223a291f04c810bce21","project":"realtime","message":"You are in a maze of dank memes, all alike."}]
            return new Models.Internal.Authenticated(json[1]["clientId"].ToString(), json[1]["channelId"].ToString());
        }

        public static Models.Internal.SessionMetadata handleSessionMetadata(JToken json)
        {
            //{"sid":"Xa70GYsr_HrF3DbQAgUW","upgrades":[],"pingInterval":25000,"pingTimeout":5000}
            return new Models.Internal.SessionMetadata(json["sid"].ToString(), int.Parse(json["pingInterval"].ToString()), int.Parse(json["pingTimeout"].ToString()));
        }
    }
}
