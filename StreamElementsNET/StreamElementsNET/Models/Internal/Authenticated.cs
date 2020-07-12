using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Internal
{
    public class Authenticated
    {
        public string ClientId { get; }
        public string ChannelId { get; }

        public Authenticated(string clientId, string channelId)
        {
            ClientId = clientId;
            ChannelId = channelId;
        }
    }
}
