using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Tip
{
    public class Tip
    {
        public string TipId { get; }
        public string Username { get; }
        public double Amount { get; }
        public string Currency { get; }
        public string Message { get; }
        public string Avatar { get; }

        public Tip(string tipId, string username, double amount, string currency, string message, string avatar)
        {
            TipId = tipId;
            Username = username;
            Amount = amount;
            Currency = currency;
            Message = message;
            Avatar = avatar;
        }
    }
}
