using System;

namespace StreamElements.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var streamElements = new StreamElementsNET.Client("<JWT-TOKEN>");

            streamElements.OnConnected += StreamElements_OnConnected;
            streamElements.OnAuthenticated += StreamElements_OnAuthenticated;
            streamElements.OnFollower += StreamElements_OnFollower;
            streamElements.OnSubscriber += StreamElements_OnSubscriber;
            streamElements.OnHost += StreamElements_OnHost;
            streamElements.OnTip += StreamElements_OnTip;
            streamElements.OnCheer += StreamElements_OnCheer;
            streamElements.OnAuthenticationFailure += StreamElements_OnAuthenticationFailure;
            streamElements.OnReceivedRawMessage += StreamElements_OnReceivedRawMessage;
            streamElements.OnSent += StreamElements_OnSent;

            streamElements.Connect();

            while (true) ;
        }

        private static void StreamElements_OnSent(object sender, string e)
        {
            Console.WriteLine($"SENT: {e}");
        }

        private static void StreamElements_OnReceivedRawMessage(object sender, string e)
        {
            Console.WriteLine($"RECEIVED: {e}");
        }

        private static void StreamElements_OnAuthenticationFailure(object sender, EventArgs e)
        {
            Console.WriteLine($"Failed to login! Invalid JWT token!");
        }

        private static void StreamElements_OnCheer(object sender, StreamElementsNET.Models.Cheer.Cheer e)
        {
            Console.WriteLine($"New cheer! From: {e.Username}, amount: {e.Amount}, message: {e.Message}");
        }

        private static void StreamElements_OnTip(object sender, StreamElementsNET.Models.Tip.Tip e)
        {
            Console.WriteLine($"New tip! From: {e.Username}, amount: ${e.Amount}, currency: {e.Currency}, message: {e.Message}");
        }

        private static void StreamElements_OnHost(object sender, StreamElementsNET.Models.Host.Host e)
        {
            Console.WriteLine($"New host! Host from: {e.Username}, viewers: {e.Amount}");
        }

        private static void StreamElements_OnSubscriber(object sender, StreamElementsNET.Models.Subscriber.Subscriber e)
        {
            Console.WriteLine($"New subscriber! Name: {e.Username}, tier: {e.Tier}, months: {e.Amount}, gifted? {e.Gifted}, gifted by: {e.Sender}");
        }

        private static void StreamElements_OnFollower(object sender, StreamElementsNET.Models.Follower.Follower e)
        {
            Console.WriteLine($"New follower! Username: {e.Username}, userid: {e.UserId}, display name: {e.DisplayName}, avatar: {e.Avatar}");
        }

        private static void StreamElements_OnAuthenticated(object sender, StreamElementsNET.Models.Internal.Authenticated e)
        {
            Console.WriteLine($"Authenticated! Using {e.ClientId} in channel {e.ChannelId}");
        }

        private static void StreamElements_OnConnected(object sender, EventArgs e)
        {
            Console.WriteLine($"Connected!");
        }
    }
}
