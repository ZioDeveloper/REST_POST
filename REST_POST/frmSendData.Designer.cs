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
            this.SuspendLayout();
            // 
            // txtHASH
            // 
            this.txtHASH.Location = new System.Drawing.Point(12, 12);
            this.txtHASH.Multiline = true;
            this.txtHASH.Name = "txtHASH";
            this.txtHASH.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHASH.Size = new System.Drawing.Size(448, 413);
            this.txtHASH.TabIndex = 2;
            // 
            // cmdSendToEcoprog
            // 
            this.cmdSendToEcoprog.Location = new System.Drawing.Point(477, 12);
            this.cmdSendToEcoprog.Name = "cmdSendToEcoprog";
            this.cmdSendToEcoprog.Size = new System.Drawing.Size(75, 37);
            this.cmdSendToEcoprog.TabIndex = 21;
            this.cmdSendToEcoprog.Text = "Send data";
            this.cmdSendToEcoprog.UseVisualStyleBackColor = true;
            this.cmdSendToEcoprog.Click += new System.EventHandler(this.SendDataToEcoprogram);
            // 
            // wfTimer
            // 
            this.wfTimer.Enabled = true;
            this.wfTimer.Interval = 20000;
            this.wfTimer.Tick += new System.EventHandler(this.wfTimer_Tick);
            // 
            // cmdSetTimer
            // 
            this.cmdSetTimer.Location = new System.Drawing.Point(575, 12);
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
            this.lblStatus.Location = new System.Drawing.Point(575, 72);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 13);
            this.lblStatus.TabIndex = 23;
            this.lblStatus.Text = "...";
            // 
            // frmSendData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}