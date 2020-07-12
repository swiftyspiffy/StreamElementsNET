using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Subscriber
{
    public class SubscriberGiftedLatest
    {
        public string Name { get; }
        public int Amount { get; }
        public string Sender { get; }
        public string Tier { get; }
        public string Message { get; }

        public SubscriberGiftedLatest(string name, int amount, string sender, string tier, string message)
        {
            Name = name;
            Amount = amount;
            Sender = sender;
            Tier = tier;
            Message = message;
        }
    }
}
