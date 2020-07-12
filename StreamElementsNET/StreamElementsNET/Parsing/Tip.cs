using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamElementsNET.Parsing
{
    internal static class Tip
    {
        public static Models.Tip.Tip handleTip(JToken json)
        {
            return new Models.Tip.Tip(json["tipId"].ToString(), json["username"].ToString(), double.Parse(json["amount"].ToString()), json["currency"].ToString(), json["message"].ToString(), json["avatar"].ToString());
        }

        public static int handleTipCount(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static Models.Tip.TipLatest handleTipLatest(JToken json)
        {
            return new Models.Tip.TipLatest(json["name"].ToString(), double.Parse(json["amount"].ToString()), json["message"].ToString());
        }

        public static double handleTipSession(JToken json)
        {
            return double.Parse(json["amount"].ToString());
        }

        public static double handleTipGoal(JToken json)
        {
            return double.Parse(json["amount"].ToString());
        }

        public static double handleTipWeek(JToken json)
        {
            return double.Parse(json["amount"].ToString());
        }

        public static double handleTipTotal(JToken json)
        {
            return double.Parse(json["amount"].ToString());
        }

        public static double handleTipMonth(JToken json)
        {
            return double.Parse(json["amount"].ToString());
        }

        public static Models.Tip.TipSessionTopDonator handleTipSessionTopDonator(JToken json)
        {
            return new Models.Tip.TipSessionTopDonator(json["name"].ToString(), double.Parse(json["amount"].ToString()));
        }

        public static Models.Tip.TipSessionTopDonation handleTipSessionTopDonation(JToken json)
        {
            return new Models.Tip.TipSessionTopDonation(json["name"].ToString(), double.Parse(json["amount"].ToString()));
        }
    }
}
