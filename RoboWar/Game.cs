using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Stats = new GameStats();
            Console.WriteLine("Initialized GameStats");
        }

        /// <summary>
        /// Checks incoming messages for commands and handles them
        /// </summary>
        /// <param name="message">IRCMessage that might contain a command</param>
        void irc_OnMessageParse(IRCMessage message)
        {
            foreach (string comm in Commands)
	        {
		        if(message.body.Contains(comm))
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

        public int CommandUp = 0;
        public int CommandDown = 0;
        public int CommandLeft = 0;
        public int CommandRight = 0;
        public int CommandShoot = 0;

        /// <summary>
        /// Initializes gamestat object
        /// </summary>
        public GameStats()
        {
            Console.WriteLine("Init Gamestats");
        }

        /// <summary>
        /// Adds a stat to the gamestats
        /// </summary>
        /// <param name="comm"></param>
        public void AddStat(string comm)
        {
            switch(comm)
            {
                case "up":
                    CommandUp++;
                    break;
                case "down":
                    CommandDown++;
                    break;
                case "left":
                    CommandLeft++;
                    break;
                case "right":
                    CommandRight++;
                    break;
                case "shoot":
                    CommandShoot++;
                    break;
                default:
                    break;
            }
            if (OnStatUpdate != null)
                OnStatUpdate(this);
        }
    }
}
