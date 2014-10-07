using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using Meebey.SmartIrc4net;

namespace RoboWar
{
    public delegate void NewMessage(IRCMessage message);

    public class IRC
    {
        public IrcClient Irc = new IrcClient();
        public List<IRCMessage> Messages = new List<IRCMessage>();
        public event NewMessage OnMessageParse;

        public IRC()
        {
            Console.WriteLine("Init");
        }

        public void OnQueryMessage(object sender, IrcEventArgs e)
        {
            switch (e.Data.MessageArray[0])
            {
                // debug stuff
                case "dump_channel":
                    string requestedChannel = e.Data.MessageArray[1];
                    // getting the channel (via channel sync feature)
                    Channel channel = Irc.GetChannel(requestedChannel);
                    // here we send messages
                    Irc.SendMessage(SendType.Message, e.Data.Nick, "<channel '" + requestedChannel + "'>");
                    Irc.SendMessage(SendType.Message, e.Data.Nick, "Name: '" + channel.Name + "'");
                    Irc.SendMessage(SendType.Message, e.Data.Nick, "Topic: '" + channel.Topic + "'");
                    Irc.SendMessage(SendType.Message, e.Data.Nick, "Mode: '" + channel.Mode + "'");
                    Irc.SendMessage(SendType.Message, e.Data.Nick, "Key: '" + channel.Key + "'");
                    Irc.SendMessage(SendType.Message, e.Data.Nick, "UserLimit: '" + channel.UserLimit + "'");
                    // here we go through all users of the channel and show their
                    // hashtable key and nickname
                    string nickname_list = "";
                    nickname_list += "Users: ";
                    foreach (DictionaryEntry de in channel.Users)
                    {
                        string key = (string)de.Key;
                        ChannelUser channeluser = (ChannelUser)de.Value;
                        nickname_list += "(";
                        if (channeluser.IsOp)
                        {
                            nickname_list += "@";
                        }
                        if (channeluser.IsVoice)
                        {
                            nickname_list += "+";
                        }
                        nickname_list += ")" + key + " => " + channeluser.Nick + ", ";
                    }
                    Irc.SendMessage(SendType.Message, e.Data.Nick, nickname_list);
                    Irc.SendMessage(SendType.Message, e.Data.Nick, "</channel>");
                    break;
                case "gc":
                    GC.Collect();
                    break;
                // typical commands
                case "join":
                    Irc.RfcJoin(e.Data.MessageArray[1]);
                    break;
                case "part":
                    Irc.RfcPart(e.Data.MessageArray[1]);
                    break;
                case "die":
                    Exit();
                    break;
            }
        }

        public void OnError(object sender, ErrorEventArgs e)
        {
            System.Console.WriteLine("Error: " + e.ErrorMessage);
            Exit();
        }

        public void OnRawMessage(object sender, IrcEventArgs e)
        {
            System.Console.WriteLine("Received: " + e.Data.RawMessage);

            IRCMessage mess = new IRCMessage(e.Data.RawMessage);

            Messages.Add(mess);

            OnMessageParse(mess);
        }

        public void Main(string nick, string chan, int port = 6667, string serverHost = "irc.twitch.tv")
        {
            // UTF-8 test
            Irc.Encoding = System.Text.Encoding.UTF8;
            // wait time between messages, we can set this lower on own irc servers
            Irc.SendDelay = 200;
            // we use channel sync, means we can use irc.GetChannel() and so on
            Irc.ActiveChannelSyncing = true;

            Irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
            Irc.OnError += new ErrorEventHandler(OnError);
            Irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);

            try
            {
                Irc.Connect(serverHost, port);
            }
            catch (ConnectionException e)
            {
                Console.WriteLine("Couldn't Connect! Reason: " + e);
                Exit();
            }

            try
            {
                Irc.Login(nick, nick);

                Irc.RfcJoin(chan);

                Irc.Listen();

                Irc.Disconnect();
            }
            catch (ConnectionException)
            {
                // this exception is handled because Disconnect() can throw a not
                // connected exception
                Exit();
            }
            catch (Exception e)
            {
                // this should not happen by just in case we handle it nicely
                System.Console.WriteLine("Error occurred! Message: " + e.Message);
                System.Console.WriteLine("Exception: " + e.StackTrace);
                Exit();
            }
        }

        public void Exit()
        {
            // we are done, lets exit
            System.Console.WriteLine("Exiting...");
            System.Environment.Exit(0);
        }

    }

    public class IRCMessage
    {
        public string User = "";
        public string Param = "";
        public string Command = "";
        public string Body = "";
        public string Full = "";

        public IRCMessage(string message)
        {
            Full = message;
            this.User = message.Split(' ')[0].Replace(':', ' ');
            this.Command = message.Split(' ')[1];
            this.Param = message.Split(' ')[2];
            try
            {
                this.Body = message.Split(':')[2];
            }
            catch (Exception)
            {
                this.Body = message;
            }
        }
    }
}
