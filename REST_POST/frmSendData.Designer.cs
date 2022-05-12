namespace REST_POST
{
    partial class frmSendData
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
            this.txtHASH = new System.Windows.Forms.TextBox();
            this.cmdSendToEcoprog = new System.Windows.Forms.Button();
            this.wfTimer = new System.Windows.Forms.Timer(this.components);
            this.cmdSetTimer = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmdClean = new System.Windows.Forms.Button();
            this.txtTimerValue = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmdCountVin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtHASH
            // 
            this.txtHASH.Location = new System.Drawing.Point(12, 12);
            this.txtHASH.Multiline = true;
            this.txtHASH.Name = "txtHASH";
            this.txtHASH.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHASH.Size = new System.Drawing.Size(346, 413);
            this.txtHASH.TabIndex = 2;
            // 
            // cmdSendToEcoprog
            // 
            this.cmdSendToEcoprog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSendToEcoprog.Location = new System.Drawing.Point(364, 99);
            this.cmdSendToEcoprog.Name = "cmdSendToEcoprog";
            this.cmdSendToEcoprog.Size = new System.Drawing.Size(75, 37);
            this.cmdSendToEcoprog.TabIndex = 21;
            this.cmdSendToEcoprog.Text = "Sending by hand !";
            this.cmdSendToEcoprog.UseVisualStyleBackColor = true;
            this.cmdSendToEcoprog.Click += new System.EventHandler(this.SendDataToEcoprogram);
            // 
            // wfTimer
            // 
            this.wfTimer.Enabled = true;
            this.wfTimer.Interval = 86400000;
            this.wfTimer.Tick += new System.EventHandler(this.wfTimer_Tick);
            // 
            // cmdSetTimer
            // 
            this.cmdSetTimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSetTimer.Location = new System.Drawing.Point(368, 12);
            this.cmdSetTimer.Name = "cmdSetTimer";
            this.cmdSetTimer.Size = new System.Drawing.Size(75, 37);
            this.cmdSetTimer.TabIndex = 22;
            this.cmdSetTimer.Text = "StartTimer";
            this.cmdSetTimer.UseVisualStyleBackColor = true;
            this.cmdSetTimer.Click += new System.EventHandler(this.cmdSetTimer_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(368, 62);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 13);
            this.lblStatus.TabIndex = 23;
            this.lblStatus.Text = "...";
            // 
            // cmdClean
            // 
            this.cmdClean.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClean.Location = new System.Drawing.Point(368, 382);
            this.cmdClean.Name = "cmdClean";
            this.cmdClean.Size = new System.Drawing.Size(75, 37);
            this.cmdClean.TabIndex = 24;
            this.cmdClean.Text = "Clean text";
            this.cmdClean.UseVisualStyleBackColor = true;
            this.cmdClean.Click += new System.EventHandler(this.cmdClean_Click);
            // 
            // txtTimerValue
            // 
            this.txtTimerValue.ForeColor = System.Drawing.Color.Red;
            this.txtTimerValue.Location = new System.Drawing.Point(369, 177);
            this.txtTimerValue.Name = "txtTimerValue";
            this.txtTimerValue.Size = new System.Drawing.Size(75, 20);
            this.txtTimerValue.TabIndex = 25;
            this.txtTimerValue.Text = "100000";
            this.txtTimerValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(368, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 24);
            this.button1.TabIndex = 26;
            this.button1.Text = "1 Day";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(369, 251);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 24);
            this.button2.TabIndex = 27;
            this.button2.Text = "1/2 Day";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdCountVin
            // 
            this.cmdCountVin.Location = new System.Drawing.Point(368, 352);
            this.cmdCountVin.Name = "cmdCountVin";
            this.cmdCountVin.Size = new System.Drawing.Size(77, 24);
            this.cmdCountVin.TabIndex = 28;
            this.cmdCountVin.Text = "Count VIN";
            this.cmdCountVin.UseVisualStyleBackColor = true;
            this.cmdCountVin.Click += new System.EventHandler(this.cmdCountVin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(368, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Milliseconds";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(369, 281);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 24);
            this.button3.TabIndex = 30;
            this.button3.Text = "4 Hours";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(368, 311);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 24);
            this.button4.TabIndex = 31;
            this.button4.Text = "2 Hours";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frmSendData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 431);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCountVin);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTimerValue);
            this.Controls.Add(this.cmdClean);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmdSetTimer);
            this.Controls.Add(this.cmdSendToEcoprog);
            this.Controls.Add(this.txtHASH);
            this.Name = "frmSendData";
            this.Text = "frmSendData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHASH;
        private System.Windows.Forms.Button cmdSendToEcoprog;
        private System.Windows.Forms.Timer wfTimer;
        private System.Windows.Forms.Button cmdSetTimer;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button cmdClean;
        private System.Windows.Forms.TextBox txtTimerValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button cmdCountVin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}