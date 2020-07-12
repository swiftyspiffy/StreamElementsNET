using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamElementsNET.Parsing
{
    internal static class Follower
    {
        public static Models.Follower.Follower handleFollower(JToken json)
        {
            return new Models.Follower.Follower(json["username"].ToString(), json["providerId"].ToString(), json["displayName"].ToString(), json["avatar"].ToString());
        }

        public static string handleFollowerLatest(JToken json)
        {
            return json["name"].ToString();
        }

        public static int handleFollowerGoal(JToken json)
        {
            return int.Parse(json["amount"].ToString());
        }

        public static int handleFollowerMonth(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static int handleFollowerWeek(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static int handleFollowerTotal(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static int handleFollowerSession(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }
    }
}
