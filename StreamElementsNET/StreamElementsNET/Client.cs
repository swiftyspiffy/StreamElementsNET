using Newtonsoft.Json.Linq;
using System;
using System.Timers;
using StreamElementsNET.Models.Unknown;
using WebSocket4Net;

namespace StreamElementsNET
{
    public class Client
    {
        private readonly string StreamElementsUrl = "wss://realtime.streamelements.com/socket.io/?cluster=main&EIO=3&transport=websocket";

        private WebSocket client;
        protected string jwt;
        private Timer pingTimer;

        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;
        public event EventHandler<SuperSocket.ClientEngine.ErrorEventArgs> OnError;
        public event EventHandler<string> OnSent;
        public event EventHandler<string> OnReceivedRawMessage;

        // authentication
        public event EventHandler<Models.Internal.Authenticated> OnAuthenticated;
        public event EventHandler OnAuthenticationFailure;

        // follower
        public event EventHandler<Models.Follower.Follower> OnFollower;
        public event EventHandler<string> OnFollowerLatest;
        public event EventHandler<int> OnFollowerGoal;
        public event EventHandler<int> OnFollowerMonth;
        public event EventHandler<int> OnFollowerWeek;
        public event EventHandler<int> OnFollowerTotal;
        public event EventHandler<int> OnFollowerSession;

        // cheer
        public event EventHandler<Models.Cheer.Cheer> OnCheer;
        public event EventHandler<Models.Cheer.CheerLatest> OnCheerLatest;
        public event EventHandler<int> OnCheerGoal;
        public event EventHandler<int> OnCheerCount;
        public event EventHandler<int> OnCheerTotal;
        public event EventHandler<int> OnCheerSession;
        public event EventHandler<Models.Cheer.CheerSessionTopDonator> OnCheerSessionTopDonator;
        public event EventHandler<Models.Cheer.CheerSessionTopDonation> OnCheerSessionTopDonation;
        public event EventHandler<int> OnCheerMonth;
        public event EventHandler<int> OnCheerWeek;

        // host
        public event EventHandler<Models.Host.Host> OnHost;
        public event EventHandler<Models.Host.HostLatest> OnHostLatest;

        // tip
        public event EventHandler<Models.Tip.Tip> OnTip;
        public event EventHandler<int> OnTipCount;
        public event EventHandler<Models.Tip.TipLatest> OnTipLatest;
        public event EventHandler<double> OnTipSession;
        public event EventHandler<double> OnTipGoal;
        public event EventHandler<double> OnTipWeek;
        public event EventHandler<double> OnTipTotal;
        public event EventHandler<double> OnTipMonth;
        public event EventHandler<Models.Tip.TipSessionTopDonator> OnTipSessionTopDonator;
        public event EventHandler<Models.Tip.TipSessionTopDonation> OnTipSessionTopDonation;

        // subscriber
        public event EventHandler<Models.Subscriber.Subscriber> OnSubscriber;
        public event EventHandler<Models.Subscriber.SubscriberLatest> OnSubscriberLatest;
        public event EventHandler<int> OnSubscriberSession;
        public event EventHandler<int> OnSubscriberGoal;
        public event EventHandler<int> OnSubscriberMonth;
        public event EventHandler<int> OnSubscriberWeek;
        public event EventHandler<int> OnSubscriberTotal;
        public event EventHandler<int> OnSubscriberPoints;
        public event EventHandler<int> OnSubscriberResubSession;
        public event EventHandler<Models.Subscriber.SubscriberResubLatest> OnSubscriberResubLatest;
        public event EventHandler<int> OnSubscriberNewSession;
        public event EventHandler<int> OnSubscriberGiftedSession;
        public event EventHandler<Models.Subscriber.SubscriberNewLatest> OnSubscriberNewLatest;
        public event EventHandler<Models.Subscriber.SubscriberAlltimeGifter> OnSubscriberAlltimeGifter;
        public event EventHandler<Models.Subscriber.SubscriberGiftedLatest> OnSubscriberGiftedLatest;

        // unknowns
        public event EventHandler<UnknownEventArgs> OnUnknownComplexObject;
        public event EventHandler<UnknownEventArgs> OnUnknownSimpleUpdate;

        public Client(string jwtToken)
        {
            jwt = jwtToken;

            client = new WebSocket(StreamElementsUrl);
            client.Opened += Client_Opened;
            client.Error += Client_Error;
            client.Closed += Client_Closed;
            client.MessageReceived += Client_MessageReceived;
        }

        public void Connect()
        {
            client.Open();
        }

        public void Disconnect()
        {
            client.Close();
        }

        public void TestMessageParsing(string message)
        {
            handleMessage(message);
        }

        private void send(string msg)
        {
            client.Send(msg);
            OnSent?.Invoke(client, msg);
        }

        private void handleAuthentication()
        {
            send($"42[\"authenticate\",{{\"method\":\"jwt\",\"token\":\"{jwt}\"}}]");
        }

        private void handlePingInitialization(Models.Internal.SessionMetadata md)
        {
            // start with a ping
            send("2");
            // start ping timer
            pingTimer = new Timer(md.PingInterval);
            pingTimer.Elapsed += PingTimer_Elapsed;
            pingTimer.Start();
        }

        private void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            handleMessage(e.Message);
        }

        private void handleMessage(string msg)
        {
            OnReceivedRawMessage?.Invoke(client, msg);
            // there's a number at the start of every message, figure out what it is, and remove it
            var raw = msg;
            if (raw.Contains("\""))
            {
                var number = msg.Split('"')[0].Substring(0, msg.Split('"')[0].Length - 1);
                raw = msg.Substring(number.Length);
            }
            if (msg.StartsWith("40"))
            {
                handleAuthentication();
                return;
            }
            if (msg.StartsWith("0{\"sid\""))
            {
                handlePingInitialization(Parsing.Internal.handleSessionMetadata(JObject.Parse(raw)));
            }
            if (msg.StartsWith("42[\"authenticated\""))
            {
                OnAuthenticated?.Invoke(client, Parsing.Internal.handleAuthenticated(JArray.Parse(raw)));
                return;
            }
            if (msg.StartsWith("42[\"unauthorized\""))
            {
                OnAuthenticationFailure?.Invoke(client, null);
            }
            if (msg.StartsWith("42[\"event\",{\"type\""))
            {
                handleComplexObject(JArray.Parse(raw));
                return;
            }
            if (msg.StartsWith("42[\"event:update\",{\"name\""))
            {
                handleSimpleUpdate(JArray.Parse(raw));
                return;
            }
        }

        private void handleComplexObject(JArray decoded)
        {
            // only handle "event" types
            if (decoded[0].ToString() != "event")
                return;
            // only handle follows from twitch
            if (decoded[1]["provider"].ToString() != "twitch")
                return;

            var type = decoded[1]["type"].ToString();
            switch (type)
            {
                case "follow":
                    OnFollower?.Invoke(client, Parsing.Follower.handleFollower(decoded[1]["data"]));
                    return;
                case "cheer":
                    OnCheer?.Invoke(client, Parsing.Cheer.handleCheer(decoded[1]["data"]));
                    return;
                case "host":
                    OnHost?.Invoke(client, Parsing.Host.handleHost(decoded[1]["data"]));
                    return;
                case "tip":
                    OnTip?.Invoke(client, Parsing.Tip.handleTip(decoded[1]["data"]));
                    return;
                case "subscriber":
                    OnSubscriber?.Invoke(client, Parsing.Subscriber.handleSubscriber(decoded[1]["data"]));
                    return;
                default:
                    OnUnknownComplexObject?.Invoke(client, new UnknownEventArgs(type, decoded[1]["data"]));
                    return;
            }
        }

        private void handleSimpleUpdate(JArray decoded)
        {
            // only handle "event:update" types
            if (decoded[0].ToString() != "event:update")
                return;
            
            var data = decoded[1]["data"];
            var type = decoded[1]["name"].ToString();
            
            switch (type)
            {
                case "follower-latest":
                    OnFollowerLatest?.Invoke(client, Parsing.Follower.handleFollowerLatest(data));
                    return;
                case "follower-goal":
                    OnFollowerGoal?.Invoke(client, Parsing.Follower.handleFollowerGoal(data));
                    return;
                case "follower-month":
                    OnFollowerMonth?.Invoke(client, Parsing.Follower.handleFollowerMonth(data));
                    return;
                case "follower-week":
                    OnFollowerWeek?.Invoke(client, Parsing.Follower.handleFollowerWeek(data));
                    return;
                case "follower-total":
                    OnFollowerTotal?.Invoke(client, Parsing.Follower.handleFollowerTotal(data));
                    return;
                case "follower-session":
                    OnFollowerSession?.Invoke(client, Parsing.Follower.handleFollowerSession(data));
                    return;
                case "cheer-latest":
                    OnCheerLatest?.Invoke(client, Parsing.Cheer.handleCheerLatest(data));
                    return;
                case "cheer-goal":
                    OnCheerGoal?.Invoke(client, Parsing.Cheer.handleCheerGoal(data));
                    return;
                case "cheer-count":
                    OnCheerCount?.Invoke(client, Parsing.Cheer.handleCheerCount(data));
                    return;
                case "cheer-total":
                    OnCheerTotal?.Invoke(client, Parsing.Cheer.handleCheerTotal(data));
                    return;
                case "cheer-session":
                    OnCheerSession?.Invoke(client, Parsing.Cheer.handleCheerSession(data));
                    return;
                case "cheer-session-top-donator":
                    OnCheerSessionTopDonator?.Invoke(client, Parsing.Cheer.handleCheerSessionTopDonator(data));
                    return;
                case "cheer-session-top-donation":
                    OnCheerSessionTopDonation?.Invoke(client, Parsing.Cheer.handleCheerSessionTopDonation(data));
                    return;
                case "cheer-month":
                    OnCheerMonth?.Invoke(client, Parsing.Cheer.handleCheerMonth(data));
                    return;
                case "cheer-week":
                    OnCheerWeek?.Invoke(client, Parsing.Cheer.handleCheerWeek(data));
                    return;
                case "host-latest":
                    OnHostLatest?.Invoke(client, Parsing.Host.handleHostLatest(data));
                    return;
                case "tip-count":
                    OnTipCount?.Invoke(client, Parsing.Tip.handleTipCount(data));
                    return;
                case "tip-latest":
                    OnTipLatest?.Invoke(client, Parsing.Tip.handleTipLatest(data));
                    return;
                case "tip-session":
                    OnTipSession?.Invoke(client, Parsing.Tip.handleTipSession(data));
                    return;
                case "tip-goal":
                    OnTipGoal?.Invoke(client, Parsing.Tip.handleTipGoal(data));
                    return;
                case "tip-week":
                    OnTipWeek?.Invoke(client, Parsing.Tip.handleTipWeek(data));
                    return;
                case "tip-total":
                    OnTipTotal?.Invoke(client, Parsing.Tip.handleTipTotal(data));
                    return;
                case "tip-month":
                    OnTipMonth?.Invoke(client, Parsing.Tip.handleTipMonth(data));
                    return;
                case "tip-session-top-donator":
                    OnTipSessionTopDonator?.Invoke(client, Parsing.Tip.handleTipSessionTopDonator(data));
                    return;
                case "tip-session-top-donation":
                    OnTipSessionTopDonation?.Invoke(client, Parsing.Tip.handleTipSessionTopDonation(data));
                    return;
                case "subscriber-latest":
                    OnSubscriberLatest?.Invoke(client, Parsing.Subscriber.handleSubscriberLatest(data));
                    return;
                case "subscriber-session":
                    OnSubscriberSession?.Invoke(client, Parsing.Subscriber.handleSubscriberSession(data));
                    return;
                case "subscriber-goal":
                    OnSubscriberGoal?.Invoke(client, Parsing.Subscriber.handleSubscriberGoal(data));
                    return;
                case "subscriber-month":
                    OnSubscriberMonth?.Invoke(client, Parsing.Subscriber.handleSubscriberMonth(data));
                    return;
                case "subscriber-week":
                    OnSubscriberWeek?.Invoke(client, Parsing.Subscriber.handleSubscriberWeek(data));
                    return;
                case "subscriber-total":
                    OnSubscriberTotal?.Invoke(client, Parsing.Subscriber.handleSubscriberTotal(data));
                    return;
                case "subscriber-points":
                    OnSubscriberPoints?.Invoke(client, Parsing.Subscriber.handleSubscriberPoints(data));
                    return;
                case "subscriber-resub-session":
                    OnSubscriberResubSession?.Invoke(client, Parsing.Subscriber.handleSubscriberResubSession(data));
                    return;
                case "subscriber-resub-latest":
                    OnSubscriberResubLatest?.Invoke(client, Parsing.Subscriber.handleSubscriberResubLatest(data));
                    return;
                case "subscriber-new-session":
                    OnSubscriberNewSession?.Invoke(client, Parsing.Subscriber.handleSubscriberNewSession(data));
                    return;
                case "subscriber-gifted-session":
                    OnSubscriberGiftedSession?.Invoke(client, Parsing.Subscriber.handleSubscriberGiftedSession(data));
                    return;
                case "subscriber-new-latest":
                    OnSubscriberNewLatest?.Invoke(client, Parsing.Subscriber.handleSubscriberNewLatest(data));
                    return;
                case "subscriber-alltime-gifter":
                    OnSubscriberAlltimeGifter?.Invoke(client, Parsing.Subscriber.handleSubscriberAlltimeGifter(data));
                    return;
                case "subscriber-gifted-latest":
                    OnSubscriberGiftedLatest?.Invoke(client, Parsing.Subscriber.handleSubscriberGiftedLatest(data));
                    return;
                default:
                    OnUnknownSimpleUpdate?.Invoke(client, new UnknownEventArgs(type, decoded[1]["data"]));
                    return;
            }
        }

        private void Client_Closed(object sender, EventArgs e)
        {
            pingTimer.Stop();
            OnDisconnected?.Invoke(sender, e);
        }

        private void Client_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            OnError?.Invoke(sender, e);
        }

        private void Client_Opened(object sender, EventArgs e)
        {
            OnConnected?.Invoke(sender, e);
        }

        private void PingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // to remain connected, we need to send a "2" every 25 seconds
            send("2");
        }
    }
}
