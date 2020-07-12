using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamElementsNET.Parsing
{
    internal static class Subscriber
    {
        public static Models.Subscriber.Subscriber handleSubscriber(JToken json)
        {
            var gifted = json["gifted"] != null;
            var sender = json["sender"] != null ? json["sender"].ToString() : "";
            var message = json["message"] != null ? json["message"].ToString() : "";
            return new Models.Subscriber.Subscriber(json["username"].ToString(), json["providerId"].ToString(), json["displayName"].ToString(),
                int.Parse(json["amount"].ToString()), json["tier"].ToString(), gifted, sender, message, json["avatar"].ToString());
        }

        public static Models.Subscriber.SubscriberLatest handleSubscriberLatest(JToken json)
        {
            var gifted = json["gifted"] != null;
            var sender = json["sender"] != null ? json["sender"].ToString() : "";
            var message = json["message"] != null ? json["message"].ToString() : "";
            return new Models.Subscriber.SubscriberLatest(json["name"].ToString(), int.Parse(json["amount"].ToString()), json["tier"].ToString(), message, gifted, sender);
        }

        public static int handleSubscriberSession(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static int handleSubscriberGoal(JToken json)
        {
            return int.Parse(json["amount"].ToString());
        }

        public static int handleSubscriberMonth(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static int handleSubscriberWeek(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static int handleSubscriberTotal(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static int handleSubscriberPoints(JToken json)
        {
            return int.Parse(json["amount"].ToString());
        }

        public static int handleSubscriberResubSession(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static Models.Subscriber.SubscriberResubLatest handleSubscriberResubLatest(JToken json)
        {
            var message = json["message"] != null ? json["message"].ToString() : "";
            return new Models.Subscriber.SubscriberResubLatest(json["name"].ToString(), int.Parse(json["amount"].ToString()), message);
        }

        public static int handleSubscriberNewSession(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static int handleSubscriberGiftedSession(JToken json)
        {
            return int.Parse(json["count"].ToString());
        }

        public static Models.Subscriber.SubscriberNewLatest handleSubscriberNewLatest(JToken json)
        {
            return new Models.Subscriber.SubscriberNewLatest(json["name"].ToString(), int.Parse(json["amount"].ToString()), json["message"].ToString());
        }

        public static Models.Subscriber.SubscriberAlltimeGifter handleSubscriberAlltimeGifter(JToken json)
        {
            return new Models.Subscriber.SubscriberAlltimeGifter(json["name"].ToString(), int.Parse(json["amount"].ToString()));
        }

        public static Models.Subscriber.SubscriberGiftedLatest handleSubscriberGiftedLatest(JToken json)
        {
            return new Models.Subscriber.SubscriberGiftedLatest(json["name"].ToString(), int.Parse(json["amount"].ToString()), json["sender"].ToString(), json["tier"].ToString(), json["message"].ToString());
        }
    }
}
