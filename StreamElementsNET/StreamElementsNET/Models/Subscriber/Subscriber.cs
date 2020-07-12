using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Subscriber
{
    public class Subscriber
    {
        public string Username { get; }
        public string UserId { get; }
        public string DisplayName { get; }
        public int Amount { get; }
        public string Tier { get; }
        public bool Gifted { get; }
        public string Sender { get; }
        public string Message { get; }
        public string Avatar { get; }

        public Subscriber(string username, string userid, string displayName, int amount, string tier, bool gifted, string sender, string message, string avatar)
        {
            Username = username;
            UserId = userid;
            DisplayName = displayName;
            Amount = amount;
            Tier = tier;
            Gifted = gifted;
            Sender = sender;
            Message = message;
            Avatar = avatar;
        }
    }
}
