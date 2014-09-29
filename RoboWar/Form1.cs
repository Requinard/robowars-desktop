using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace RoboWar
{
    public partial class Form1 : Form
    {

        public delegate void UpdateControl(object sender, IRCMessage message);
        public delegate void UpdateStatsDel(GameStats stats);
        public IRC irc;
        public Game game;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (irc != null)
                irc.Exit();

            irc = new IRC();

            irc.OnMessageParse +=irc_OnMessageParse;

            Thread t = new Thread(startIRC);

            t.Start();
        }

        public void startIRC()
        {
            irc.Main(text_nick.Text, text_chan.Text, server_host: text_host.Text);
        }

        public void UpdateMessage(object sender, IRCMessage message)
        {
            text_log.Text = message.full + "\r\n" + text_log.Text;
        }

        public void UpdateStats(GameStats stats)
        {
            num_up.Value = stats.command_up;
            num_down.Value = stats.command_down;
            num_left.Value = stats.command_left;
            num_right.Value = stats.command_right;
            num_shoot.Value = stats.command_shoot;
        }

        void irc_OnMessageParse(IRCMessage message)
        {
            UpdateControl d = new UpdateControl(UpdateMessage);
            this.Invoke(d, new object[] { this, message });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (irc != null)
                irc.Exit();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            game = new Game(irc);
            game.stats.OnStatUpdate += stats_OnStatUpdate;
        }

        void stats_OnStatUpdate(GameStats stats)
        {
            UpdateStatsDel d = new UpdateStatsDel(UpdateStats);
            this.Invoke(d, new object[] { stats });
        }
    }
}
