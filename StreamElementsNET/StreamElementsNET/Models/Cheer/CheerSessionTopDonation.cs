using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Cheer
{
    public class CheerSessionTopDonation
    {
        public string Name { get; }
        public int Amount { get; }
        public string Message { get; }

        public CheerSessionTopDonation(string name, int amount, string message)
        {
            Name = name;
            Amount = amount;
            Message = message;
        }
    }
}
