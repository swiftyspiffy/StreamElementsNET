using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamElementsNET.Models.Internal
{
    public class SessionMetadata
    {
        public string SID { get; }
        public int PingInterval { get; }
        public int PingTimeout { get; }

        public SessionMetadata(string sid, int pingInterval, int pingTimeout)
        {
            SID = sid;
            PingInterval = pingInterval;
            PingTimeout = pingTimeout;
        }
    }
}
