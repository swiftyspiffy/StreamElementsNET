using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamElementsNET.Parsing
{
    internal static class Host
    {
        public static Models.Host.Host handleHost(JToken json)
        {
            return new Models.Host.Host(json["username"].ToString(), json["providerId"].ToString(), json["displayName"].ToString(), int.Parse(json["amount"].ToString()), json["avatar"].ToString());
        }

        public static Models.Host.HostLatest handleHostLatest(JToken json)
        {
            return new Models.Host.HostLatest(json["name"].ToString(), int.Parse(json["amount"].ToString()));
        }
    }
}
