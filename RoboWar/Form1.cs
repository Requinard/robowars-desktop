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
        public EV3Messenger Robot;

        public Form1()
        {
            InitializeComponent();

            foreach (string port in SerialPort.GetPortNames())
            {
                combo_com_ports.Items.Add(port);
            }
            timer1.Interval = (int)numericDelayForCommand.Value;
            timer1.Tick += timer1_Tick;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Starts a thread to handle IRC communications
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments for click</param>
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

        /// <summary>
        /// Function that starts the main IRC loop
        /// </summary>
        public void StartIrc()
        {
            Irc.Main(text_nick.Text, text_chan.Text, serverHost: text_host.Text);
        }

        /// <summary>
        /// Logs a text message to the console
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="message">IRCMessage that was received</param>
        public void UpdateMessage(object sender, IRCMessage message)
        {
            text_log.Text = string.Format("{0}\r\n{1}", message.Full, text_log.Text);
        }


        /// <summary>
        /// Synchronizes the game stats with the form
        /// </summary>
        /// <param name="stats">Stats that are sent</param>
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

            if (Robot != null)
                Robot.Disconnect();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (Game == null)
            {
                button_start.Text = "Stop the game";
                Game = new Game(Irc);
                Game.Stats.OnStatUpdate += stats_OnStatUpdate;

                timer1.Start();
            }
            else
            {
                timer1.Stop();
                Game = null;
                button_start.Text = "Start the game";
            }

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
                Robot = new EV3Messenger();
                Robot.Connect(combo_com_ports.Text);

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

        private void button2_Click(object sender, EventArgs e)
        {
            Game.Stats.GetPopularCommand();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            int command = Game.Stats.GetPopularCommand();

            if(command != 100)
                Robot.SendMessage("Control", command);
        }
    }
}
