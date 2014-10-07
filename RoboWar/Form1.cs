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
        public IRC Irc;
        public Game Game;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (Irc != null)
                Irc.Exit();

            Irc = new IRC();

            Irc.OnMessageParse +=irc_OnMessageParse;

            Thread t = new Thread(startIRC);

            t.Start();
        }

        public void startIRC()
        {
            Irc.Main(text_nick.Text, text_chan.Text, server_host: text_host.Text);
        }

        public void UpdateMessage(object sender, IRCMessage message)
        {
            text_log.Text = string.Format("{0}\r\n{1}", message.full, text_log.Text);
        }

        public void UpdateStats(GameStats stats)
        {
            num_up.Value = stats.CommandUp;
            num_down.Value = stats.CommandDown;
            num_left.Value = stats.CommandLeft;
            num_right.Value = stats.CommandRight;
            num_shoot.Value = stats.CommandShoot;
        }

        void irc_OnMessageParse(IRCMessage message)
        {
            UpdateControl d = new UpdateControl(UpdateMessage);
            this.Invoke(d, new object[] { this, message });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Irc != null)
                Irc.Exit();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            Game = new Game(Irc);
            Game.Stats.OnStatUpdate += stats_OnStatUpdate;
        }

        void stats_OnStatUpdate(GameStats stats)
        {
            UpdateStatsDel d = new UpdateStatsDel(UpdateStats);
            this.Invoke(d, new object[] { stats });
        }
    }
}
