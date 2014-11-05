using System;
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

        private string chan;

        public IRC()
        {
            Console.WriteLine(@"Init");
        }

        /// <summary>
        /// Handles special Query Messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnQueryMessage(object sender, IrcEventArgs e)
        {
            string nickname_list = "";
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

        /// <summary>
        /// Handles errors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error: {0}", e.ErrorMessage);
            Exit();
        }

        /// <summary>
        /// Sends a message over IRC
        /// </summary>
        /// <param name="message">Contents of the message</param>
        /// <param name="chan">Channel you'll be sending it to</param>
        public void SendMessage(string message)
        {
            Irc.SendMessage(SendType.Message,chan, message);
        }

        /// <summary>
        /// Handle regular messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnRawMessage(object sender, IrcEventArgs e)
        {
            Console.WriteLine("Received: {0}", e.Data.RawMessage);

            IRCMessage mess = new IRCMessage(e.Data.RawMessage);

            Messages.Add(mess);

            OnMessageParse(mess);
        }
        /// <summary>
        /// Initialize all IRC work
        /// </summary>
        /// <param name="nick">the nickname you want</param>
        /// <param name="chan">Channel you want to join</param>
        /// <param name="port">Port to connect on</param>
        /// <param name="serverHost">Hostname of the server to connect to. IPs are allowed</param>
        public void Main(string nick, string chan, int port = 6667, string serverHost = "irc.twitch.tv", string oath = null)
        {
            this.chan = chan;
            // UTF-8 test
            Irc.Encoding = System.Text.Encoding.UTF8;
            // wait time between messages, we can set this lower on own irc servers
            Irc.SendDelay = 200;
            // we use channel sync, means we can use irc.GetChannel() and so on
            Irc.ActiveChannelSyncing = true;

            Irc.OnRawMessage += OnRawMessage;
            Irc.OnError += OnError;
            Irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);

            try
            {
                Irc.Connect(serverHost, port);
            }
            catch (ConnectionException e)
            {
                Console.WriteLine("Couldn't Connect! Reason: {0}", e);
                Exit();
            }

            try
            {
                if(oath!= null)
                    Irc.Login(nick, nick, 1, nick, oath);
                else
                {
                    Irc.Login(nick, nick);
                }

                Irc.RfcJoin(chan);

                Irc.SendMessage(SendType.Message, chan,"Hi guys, Welcome to TwitchWars!");
                while (true)
                {
                    try
                    {
                        Irc.Listen();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                

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
                Console.WriteLine(@"Error occurred! Message: {0}", e.Message);
                Console.WriteLine(@"Exception: {0}", e.StackTrace);
                Exit();
            }
        }

        public void Exit()
        {
            // we are done, lets exit
            Console.WriteLine(@"Exiting...");
            Environment.Exit(0);
        }

    }

// ReSharper disable once InconsistentNaming
    public class IRCMessage
    {
        public string User = "";
        public string Param = "";
        public string Command = "";
        public string Body = "";
        public string Full = "";

        /// <summary>
        /// Parse a message as an IRC message object
        /// </summary>
        /// <param name="message"></param>
        public IRCMessage(string message)
        {
            Full = message;
            User = message.Split(' ')[0].Replace(':', ' ');
            Command = message.Split(' ')[1];
            Param = message.Split(' ')[2];
            try
            {
                Body = message.Split(':')[2];
            }
            catch (Exception)
            {
                Body = message;
            }
        }
    }
}
