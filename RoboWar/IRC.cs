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
        public IrcClient irc = new IrcClient();
        public List<IRCMessage> messages = new List<IRCMessage>();
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
                    string requested_channel = e.Data.MessageArray[1];
                    // getting the channel (via channel sync feature)
                    Channel channel = irc.GetChannel(requested_channel);
                    // here we send messages
                    irc.SendMessage(SendType.Message, e.Data.Nick, "<channel '" + requested_channel + "'>");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Name: '" + channel.Name + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Topic: '" + channel.Topic + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Mode: '" + channel.Mode + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Key: '" + channel.Key + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "UserLimit: '" + channel.UserLimit + "'");
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
                    irc.SendMessage(SendType.Message, e.Data.Nick, nickname_list);
                    irc.SendMessage(SendType.Message, e.Data.Nick, "</channel>");
                    break;
                case "gc":
                    GC.Collect();
                    break;
                // typical commands
                case "join":
                    irc.RfcJoin(e.Data.MessageArray[1]);
                    break;
                case "part":
                    irc.RfcPart(e.Data.MessageArray[1]);
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

            messages.Add(mess);

            OnMessageParse(mess);
        }

        public void Main(string nick, string chan, int port = 6667, string server_host = "irc.twitch.tv")
        {
            // UTF-8 test
            irc.Encoding = System.Text.Encoding.UTF8;
            // wait time between messages, we can set this lower on own irc servers
            irc.SendDelay = 200;
            // we use channel sync, means we can use irc.GetChannel() and so on
            irc.ActiveChannelSyncing = true;

            irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
            irc.OnError += new ErrorEventHandler(OnError);
            irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);

            try
            {
                irc.Connect(server_host, port);
            }
            catch (ConnectionException e)
            {
                Console.WriteLine("Couldn't Connect! Reason: " + e);
                Exit();
            }

            try
            {
                irc.Login(nick, nick);

                irc.RfcJoin(chan);

                irc.Listen();

                irc.Disconnect();
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

        public static void Exit()
        {
            // we are done, lets exit
            System.Console.WriteLine("Exiting...");
            System.Environment.Exit(0);
        }

    }

    public class IRCMessage
    {
        public string user = "";
        public string param = "";
        public string command = "";
        public string body = "";
        public string full = "";

        public IRCMessage(string message)
        {
            full = message;
            this.user = message.Split(' ')[0].Replace(':', ' ');
            this.command = message.Split(' ')[1];
            this.param = message.Split(' ')[2];
            try
            {
                this.body = message.Split(':')[2];
            }
            catch (Exception)
            {
                this.body = message;
            }
        }
    }
}
