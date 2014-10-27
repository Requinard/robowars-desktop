namespace RoboWar
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.text_host = new System.Windows.Forms.TextBox();
            this.text_nick = new System.Windows.Forms.TextBox();
            this.text_chan = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.text_oauth = new System.Windows.Forms.TextBox();
            this.text_log = new System.Windows.Forms.TextBox();
            this.button_start = new System.Windows.Forms.Button();
            this.Stats = new System.Windows.Forms.GroupBox();
            this.num_shoot = new System.Windows.Forms.NumericUpDown();
            this.num_right = new System.Windows.Forms.NumericUpDown();
            this.num_left = new System.Windows.Forms.NumericUpDown();
            this.num_down = new System.Windows.Forms.NumericUpDown();
            this.num_up = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.combo_com_ports = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numericDelayForCommand = new System.Windows.Forms.NumericUpDown();
            this.Stats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_shoot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_up)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDelayForCommand)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Chan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nick";
            // 
            // text_host
            // 
            this.text_host.Location = new System.Drawing.Point(67, 9);
            this.text_host.Name = "text_host";
            this.text_host.Size = new System.Drawing.Size(100, 20);
            this.text_host.TabIndex = 4;
            this.text_host.Text = "irc.freenode.com";
            // 
            // text_nick
            // 
            this.text_nick.Location = new System.Drawing.Point(67, 33);
            this.text_nick.Name = "text_nick";
            this.text_nick.Size = new System.Drawing.Size(100, 20);
            this.text_nick.TabIndex = 5;
            this.text_nick.Text = "twitchbot1";
            // 
            // text_chan
            // 
            this.text_chan.Location = new System.Drawing.Point(67, 57);
            this.text_chan.Name = "text_chan";
            this.text_chan.Size = new System.Drawing.Size(100, 20);
            this.text_chan.TabIndex = 6;
            this.text_chan.Text = "#twitchwars1";
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(18, 110);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(152, 23);
            this.button_connect.TabIndex = 7;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "OAUTH";
            // 
            // text_oauth
            // 
            this.text_oauth.Enabled = false;
            this.text_oauth.Location = new System.Drawing.Point(67, 84);
            this.text_oauth.Name = "text_oauth";
            this.text_oauth.Size = new System.Drawing.Size(100, 20);
            this.text_oauth.TabIndex = 9;
            // 
            // text_log
            // 
            this.text_log.Location = new System.Drawing.Point(174, 9);
            this.text_log.Multiline = true;
            this.text_log.Name = "text_log";
            this.text_log.ReadOnly = true;
            this.text_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_log.Size = new System.Drawing.Size(626, 241);
            this.text_log.TabIndex = 10;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(18, 140);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(149, 23);
            this.button_start.TabIndex = 11;
            this.button_start.Text = "Start Game";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // Stats
            // 
            this.Stats.Controls.Add(this.num_shoot);
            this.Stats.Controls.Add(this.num_right);
            this.Stats.Controls.Add(this.num_left);
            this.Stats.Controls.Add(this.num_down);
            this.Stats.Controls.Add(this.num_up);
            this.Stats.Controls.Add(this.label9);
            this.Stats.Controls.Add(this.label8);
            this.Stats.Controls.Add(this.label7);
            this.Stats.Controls.Add(this.label6);
            this.Stats.Controls.Add(this.label5);
            this.Stats.Location = new System.Drawing.Point(12, 256);
            this.Stats.Name = "Stats";
            this.Stats.Size = new System.Drawing.Size(200, 114);
            this.Stats.TabIndex = 12;
            this.Stats.TabStop = false;
            this.Stats.Text = "groupBox1";
            // 
            // num_shoot
            // 
            this.num_shoot.Location = new System.Drawing.Point(55, 88);
            this.num_shoot.Name = "num_shoot";
            this.num_shoot.Size = new System.Drawing.Size(120, 20);
            this.num_shoot.TabIndex = 9;
            // 
            // num_right
            // 
            this.num_right.Location = new System.Drawing.Point(55, 71);
            this.num_right.Name = "num_right";
            this.num_right.Size = new System.Drawing.Size(120, 20);
            this.num_right.TabIndex = 8;
            // 
            // num_left
            // 
            this.num_left.Location = new System.Drawing.Point(55, 54);
            this.num_left.Name = "num_left";
            this.num_left.Size = new System.Drawing.Size(120, 20);
            this.num_left.TabIndex = 7;
            // 
            // num_down
            // 
            this.num_down.Location = new System.Drawing.Point(55, 37);
            this.num_down.Name = "num_down";
            this.num_down.Size = new System.Drawing.Size(120, 20);
            this.num_down.TabIndex = 6;
            // 
            // num_up
            // 
            this.num_up.Location = new System.Drawing.Point(55, 18);
            this.num_up.Name = "num_up";
            this.num_up.Size = new System.Drawing.Size(120, 20);
            this.num_up.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Shoot";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Right";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Left";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Down";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Up";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.combo_com_ports);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(219, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // combo_com_ports
            // 
            this.combo_com_ports.FormattingEnabled = true;
            this.combo_com_ports.Location = new System.Drawing.Point(55, 17);
            this.combo_com_ports.Name = "combo_com_ports";
            this.combo_com_ports.Size = new System.Drawing.Size(139, 21);
            this.combo_com_ports.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "COM";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // numericDelayForCommand
            // 
            this.numericDelayForCommand.Location = new System.Drawing.Point(426, 268);
            this.numericDelayForCommand.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericDelayForCommand.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericDelayForCommand.Name = "numericDelayForCommand";
            this.numericDelayForCommand.Size = new System.Drawing.Size(120, 20);
            this.numericDelayForCommand.TabIndex = 14;
            this.numericDelayForCommand.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 378);
            this.Controls.Add(this.numericDelayForCommand);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Stats);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.text_log);
            this.Controls.Add(this.text_oauth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.text_chan);
            this.Controls.Add(this.text_nick);
            this.Controls.Add(this.text_host);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Twitch Plays Robowars";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Stats.ResumeLayout(false);
            this.Stats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_shoot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_up)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDelayForCommand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_host;
        private System.Windows.Forms.TextBox text_nick;
        private System.Windows.Forms.TextBox text_chan;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_oauth;
        private System.Windows.Forms.TextBox text_log;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.GroupBox Stats;
        private System.Windows.Forms.NumericUpDown num_shoot;
        private System.Windows.Forms.NumericUpDown num_right;
        private System.Windows.Forms.NumericUpDown num_left;
        private System.Windows.Forms.NumericUpDown num_down;
        private System.Windows.Forms.NumericUpDown num_up;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox combo_com_ports;
        private System.Windows.Forms.Label label10;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numericDelayForCommand;
    }
}

