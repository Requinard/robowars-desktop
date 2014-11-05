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
        public IRC Irc = null;
        public Game Game = null;
        public EV3Messenger Robot = null;

        /// <summary>
        /// Initialize the form
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            foreach (string port in SerialPort.GetPortNames())
            {
                combo_com_ports.Items.Add(port);
            }

            combo_com_ports.SelectedIndex = 0;
            timer1.Interval = (int)numericDelayForCommand.Value;
            timer1.Tick += timer1_Tick;

            text_how_to.Text =
                "First, connect to your robot using EV3, and try sending a message to the Control channel, with a 0.\r\n If this doesn't work, move on to a different com port.\r\n After that, fill the com port box with the desired port.\r\n Press connect to IRC, and then connect to robot. Then start the game.";
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
            Irc.Main(text_nick.Text, text_chan.Text, 6667, text_host.Text, text_oauth.Text);
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

        /// <summary>
        /// Invokes a message update
        /// </summary>
        /// <param name="message"></param>
        void irc_OnMessageParse(IRCMessage message)
        {
            UpdateControl d = UpdateMessage;
            Invoke(d, new object[] { this, message });
        }

        /// <summary>
        /// Disconnect from IRC and Robot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Irc != null)
                Irc.SendMessage("Well, that's it for this time. Bye!");
                Irc.Exit();

            if (Robot != null)
                Robot.Disconnect();
        }

        /// <summary>
        /// Start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_start_Click(object sender, EventArgs e)
        {
            if (Game == null)
            {
                if (Irc == null)
                {
                    MessageBox.Show("Connect to IRC first!");
                    return;
                }
                if (Robot == null)
                {
                    MessageBox.Show("Not connected to robot!");
                    return;
                }
                button_start.Text = "Stop the game";
                Game = new Game(Irc, checkUseRandom.Checked);
                Game.Stats.OnStatUpdate += stats_OnStatUpdate;

                timer1.Start();
            }
            else
            {
                timer1.Stop();
                Game = null;
                Irc.SendMessage("That's it for this round! I'll be waiting until my operator sends his next command");
                button_start.Text = "Start the game";
            }

        }
        /// <summary>
        /// Update stats of on the form
        /// </summary>
        /// <param name="stats"></param>
        void stats_OnStatUpdate(GameStats stats)
        {
            var d = new UpdateStatsDel(UpdateStats);
            Invoke(d, new object[] { stats });
        }

        /// <summary>
        /// Connect to a robot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Robot = new EV3Messenger();
                Robot.Connect(combo_com_ports.Text);
                MessageBox.Show("Successfully connected to Robot");
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Connection Failed!");
                Console.WriteLine(ex.ToString());
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(serialPort1.ReadLine());
        }

        /// <summary>
        /// Get popular command by pressing a button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Game.Stats.GetPopularCommand();
        }


        /// <summary>
        /// Handle timer ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            int command = Game.Stats.GetPopularCommand();

            if(command != 100)
                Robot.SendMessage("Control", command);
        }

        private void numericDelayForCommand_ValueChanged(object sender, EventArgs e)
        {
            bool reenable = false;

            if (timer1.Enabled)
            {
                timer1.Stop();
                reenable = true;
            }

            timer1.Interval = (int) numericDelayForCommand.Value;

            if(reenable)
                timer1.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
