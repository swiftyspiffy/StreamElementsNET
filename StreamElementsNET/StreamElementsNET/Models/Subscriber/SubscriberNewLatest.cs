using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Subscriber
{
    public class SubscriberNewLatest
    {
        public string Name { get; }
        public int Amount { get; }
        public string Message { get; }

        public SubscriberNewLatest(string name, int amount, string message)
        {
            Name = name;
            Amount = amount;
            Message = message;
        }
    }
}
