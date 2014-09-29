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
        public IRC irc;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {

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

        void irc_OnMessageParse(IRCMessage message)
        {
            UpdateControl d = new UpdateControl(UpdateMessage);
            this.Invoke(d, new object[] { this, message });
        }
    }
}
