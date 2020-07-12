using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Tip
{
    public class TipSessionTopDonator
    {
        public string Name { get; }
        public double Amount { get; }

        public TipSessionTopDonator(string name, double amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
