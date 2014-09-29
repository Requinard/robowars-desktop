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
            this.button_connect.Location = new System.Drawing.Point(15, 127);
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
            this.text_oauth.Location = new System.Drawing.Point(67, 84);
            this.text_oauth.Name = "text_oauth";
            this.text_oauth.Size = new System.Drawing.Size(100, 20);
            this.text_oauth.TabIndex = 9;
            this.text_oauth.Text = "oauth:ioya361ilws9p7m3ddapp4w5rwr3i11";
            // 
            // text_log
            // 
            this.text_log.Location = new System.Drawing.Point(174, 9);
            this.text_log.Multiline = true;
            this.text_log.Name = "text_log";
            this.text_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_log.Size = new System.Drawing.Size(626, 241);
            this.text_log.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 262);
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
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

