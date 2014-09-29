using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboWar
{
    public class Game
    {
        public List<string> Commands = new List<string>()
        {
            "up",
            "down",
            "left",
            "right",
            "shoot",
            "koen"
        };

        public GameStats stats { get; private set; }
        private IRC irc;

        /// <summary>
        /// Sets up game status
        /// </summary>
        /// <param name="irc">IRC object that commands will come from</param>
        public Game(IRC irc)
        {
            Console.WriteLine("Initializing Game");

            this.irc = irc;
            Console.WriteLine("Got IRC");

            irc.OnMessageParse += irc_OnMessageParse;
            Console.WriteLine("Added Message Parsing Method");

            stats = new GameStats();
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
                    stats.AddStat(comm);
                }
	        }
        }

    }

    public class GameStats
    {
        public delegate void GameStatsUpdated(GameStats stats);
        public event GameStatsUpdated OnStatUpdate;

        public int command_up = 0;
        public int command_down = 0;
        public int command_left = 0;
        public int command_right = 0;
        public int command_shoot = 0;

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
                    command_up++;
                    break;
                case "down":
                    command_down++;
                    break;
                case "left":
                    command_left++;
                    break;
                case "right":
                    command_right++;
                    break;
                case "shoot":
                    command_shoot++;
                    break;
                default:
                    break;
            }
            if (OnStatUpdate != null)
                OnStatUpdate(this);
        }
    }
}
