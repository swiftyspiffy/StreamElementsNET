using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Cheer
{
    public class CheerLatest
    {
        public string Name { get; }
        public int Amount { get; }
        public string Message { get; }

        public CheerLatest(string name, int amount, string message)
        {
            Name = name;
            Amount = amount;
            Message = message;
        }
    }
}
