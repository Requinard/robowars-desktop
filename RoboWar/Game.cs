using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;

namespace RoboWar
{
    public class Game
    {
        public List<string> Commands;

        public GameStats Stats { get; private set; }

        public IRC Irc
        {
            get { return _irc; }
        }

        private IRC _irc;

        /// <summary>
        /// Sets up game status
        /// </summary>
        /// <param name="irc">IRC object that commands will come from</param>
        public Game(IRC irc)
        {
            Commands = new List<string>()
            {
                "up",
                "down",
                "left",
                "right",
                "shoot",
                "koen"
            };
            Console.WriteLine("Initializing Game");

            this._irc = irc;
            Console.WriteLine("Got IRC");

            irc.OnMessageParse += irc_OnMessageParse;
            Console.WriteLine("Added Message Parsing Method");

            Stats = new GameStats(this.Commands);
            Console.WriteLine("Initialized GameStats");

            //TODO: fix channel
            this._irc.SendMessage("The game is on!", "#twitchwars1");
        }

        /// <summary>
        /// Checks incoming messages for commands and handles them
        /// </summary>
        /// <param name="message">IRCMessage that might contain a command</param>
        void irc_OnMessageParse(IRCMessage message)
        {
            foreach (string comm in Commands)
	        {
		        if(message.Body.Contains(comm))
                {
                    Stats.AddStat(comm);
                }
	        }
        }

    }

    public class GameStats
    {
        public delegate void GameStatsUpdated(GameStats stats);
        public event GameStatsUpdated OnStatUpdate;
        private bool hanglock = false;

        public int CommandUp = 0;
        public int CommandDown = 0;
        public int CommandLeft = 0;
        public int CommandRight = 0;
        public int CommandShoot = 0;

        public Dictionary<string, int> Commands = new Dictionary<string, int>();

        public List<string> CommandList;

        private void clearCommandList()
        {
            hanglock = true;

            Commands = new Dictionary<string, int>();

            foreach (string comm in CommandList)
            {
                Commands[comm] = 0;
            }

            hanglock = false;
        }

        /// <summary>
        /// Initializes gamestat object
        /// </summary>
        public GameStats(List<string> commands )
        {
            Console.WriteLine("Init Gamestats");

            CommandList = commands;

            foreach (string command in commands)
            {
                Commands[command] = 0;
            }
        }

        /// <summary>
        /// Adds a stat to the gamestats
        /// </summary>
        /// <param name="comm"></param>
        public void AddStat(string comm)
        {
            if(hanglock)
                return;

            switch(comm)
            {
                case "up":
                    ++CommandUp;
                    ++Commands[comm];
                    break;
                case "down":
                    ++CommandDown;
                    ++Commands[comm];
                    break;
                case "left":
                    ++CommandLeft;
                    ++Commands[comm];
                    break;
                case "right":
                    ++CommandRight;
                    ++Commands[comm];
                    break;
                case "shoot":
                    ++CommandShoot;
                    ++Commands[comm];
                    break;
                default:
                    break;
            }
            if (OnStatUpdate != null)
                OnStatUpdate(this);
        }

        public int GetPopularCommand()
        {
            int val = Commands.Values.Max();
            string command_end = "";
            bool allAreNull = true;

            foreach (string command in Commands.Keys)
            {
                if (Commands[command] > 0)
                    allAreNull = false;
                    
                if (Commands[command] == val)
                {
                    command_end = command;
                    break;
                }
            }
            int commandNumber = CommandList.IndexOf(command_end);

            clearCommandList();

            if (allAreNull)
            {
                return 100;
            }

           

            return commandNumber;
        }
    }
}
