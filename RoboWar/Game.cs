﻿using System;
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

        public IRC Irc;

        /// <summary>
        /// Sets up game status
        /// </summary>
        /// <param name="irc">IRC object that commands will come from</param>
        public Game(IRC irc, bool useRandomCommand)
        {
            // Commando's hier toevoegen
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

            this.Irc = irc;
            Console.WriteLine("Got IRC");

            irc.OnMessageParse += irc_OnMessageParse;
            Console.WriteLine("Added Message Parsing Method");

            Stats = new GameStats(this.Commands, useRandomCommand);
            Console.WriteLine("Initialized GameStats");

            //TODO: fix channel
            this.Irc.SendMessage("The game is on!");
        }

        /// <summary>
        /// Checks incoming messages for commands and handles them
        /// </summary>
        /// <param name="message">IRCMessage that might contain a command</param>
        void irc_OnMessageParse(IRCMessage message)
        {
            foreach (string comm in Commands)
	        {
		        if(message.Body.StartsWith(comm))
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
        private bool useRandomCommand;
        private Random rand;

        public int CommandUp = 0;
        public int CommandDown = 0;
        public int CommandLeft = 0;
        public int CommandRight = 0;
        public int CommandShoot = 0;

        public Dictionary<string, int> Commands = new Dictionary<string, int>();

        public List<string> CommandList;

        /// <summary>
        /// Clears all command counts and rebuilds the list
        /// </summary>
        private void clearCommandList()
        {
            if (hanglock)
            {
                while (hanglock)
                    continue;
            }

            hanglock = true;

            Commands = new Dictionary<string, int>();

            foreach (var command in CommandList)
            {
                Commands[command] = 0;
            }

            hanglock = false;

            return;
        }

        /// <summary>
        /// Initializes gamestat object
        /// </summary>
        public GameStats(List<string> commands, bool useRandomCommand )
        {
            Console.WriteLine("Init Gamestats");

            CommandList = commands;

            rand = new Random();

            this.useRandomCommand = useRandomCommand;

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

            hanglock = true;

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

            hanglock = false;

            return;
        }


        /// <summary>
        /// Gets the command with the most votes
        /// </summary>
        /// <returns>Integer of command index</returns>
        public int GetPopularCommand()
        {
            // Get the maximum value, so we know what key to look for
            int val = Commands.Values.Max();
            string commandBuffer = "";
            bool allAreNull = true;

            //Iterate through the keys to find the correct values
            foreach (string command in Commands.Keys)
            {
                // See if a command is above zero
                if (Commands[command] > 0)
                    allAreNull = false;
                    
                if (Commands[command] == val)
                {
                    commandBuffer = command;
                    break;
                }
            }
            // Get the index of the command
            int commandNumber = CommandList.IndexOf(commandBuffer);

            //Reset commandlist
            clearCommandList();

            //If all commands are null, send 100 as an error code
            if (allAreNull)
            {
                if (useRandomCommand)
                    return rand.Next(0, CommandList.Count);
                else
                    return 100;
            }

            return commandNumber;
        }
    }
}
