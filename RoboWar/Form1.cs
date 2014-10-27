using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using EV3MessengerLib;

namespace RoboWar
{
    public partial class Form1 : Form
    {

        public delegate void UpdateControl(object sender, IRCMessage message);
        public delegate void UpdateStatsDel(GameStats stats);
        public IRC Irc;
        public Game Game;
        public EV3Messenger robot;

        public Form1()
        {
            InitializeComponent();

            foreach (string port in SerialPort.GetPortNames())
            {
                combo_com_ports.Items.Add(port);
            }
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (Irc != null)
                Irc.Exit();

            Irc = new IRC();

            Irc.OnMessageParse +=irc_OnMessageParse;

            // ReSharper disable once SuggestUseVarKeywordEvident
            Thread t = new Thread(StartIrc);

            t.Start();
        }

        public void StartIrc()
        {
            Irc.Main(text_nick.Text, text_chan.Text, serverHost: text_host.Text);
        }

        public void UpdateMessage(object sender, IRCMessage message)
        {
            text_log.Text = string.Format("{0}\r\n{1}", message.Full, text_log.Text);
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
            UpdateControl d = UpdateMessage;
            Invoke(d, new object[] { this, message });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Irc != null)
                Irc.Exit();

            if (robot != null)
                robot.Disconnect();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            Game = new Game(Irc);
            Game.Stats.OnStatUpdate += stats_OnStatUpdate;
        }

        void stats_OnStatUpdate(GameStats stats)
        {
            var d = new UpdateStatsDel(UpdateStats);
            Invoke(d, new object[] { stats });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string item in System.IO.Ports.SerialPort.GetPortNames())
            {
                combo_com_ports.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                robot = new EV3Messenger();
                robot.Connect(combo_com_ports.Text);

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(serialPort1.ReadLine());
        }
    }
}
