using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Host
{
    public class HostLatest
    {
        public string Name { get; }
        public int Amount { get; }

        public HostLatest(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
