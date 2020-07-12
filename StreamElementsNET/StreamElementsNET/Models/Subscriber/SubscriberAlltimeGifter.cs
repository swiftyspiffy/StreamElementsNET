using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Subscriber
{
    public class SubscriberAlltimeGifter
    {
        public string Name { get; }
        public int Amount { get; }

        public SubscriberAlltimeGifter(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
