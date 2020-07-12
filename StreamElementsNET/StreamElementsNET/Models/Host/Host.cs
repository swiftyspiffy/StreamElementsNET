using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Host
{
    public class Host
    {
        public string Username { get; }
        public string UserId { get; }
        public string DisplayName { get; }
        public int Amount { get; }
        public string Avatar { get; }

        public Host(string username, string userId, string displayName, int amount, string avatar)
        {
            Username = username;
            UserId = userId;
            DisplayName = displayName;
            Amount = amount;
            Avatar = avatar;
        }
    }
}
