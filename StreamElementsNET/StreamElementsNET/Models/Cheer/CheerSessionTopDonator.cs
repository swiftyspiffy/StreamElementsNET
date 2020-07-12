using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Cheer
{
    public class CheerSessionTopDonator
    {
        public string Name { get; }
        public int Amount { get; }

        public CheerSessionTopDonator(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
