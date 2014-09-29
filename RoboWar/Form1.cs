using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboWar
{
    public partial class Form1 : Form
    {
        public IRC irc;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            
            irc = new IRC();

            irc.Main(text_nick.Text, text_chan.Text, server_host: text_host.Text);
        }
    }
}
