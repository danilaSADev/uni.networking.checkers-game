using System.ComponentModel;

namespace CheckersClient.Forms
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.playerSide = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timeForTurnLeftLabel = new System.Windows.Forms.Label();
            this.winScreenLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::CheckersClient.Properties.Resources.checkers_board;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(61, 63);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 320);
            this.panel1.TabIndex = 0;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 3;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.Location = new System.Drawing.Point(401, 73);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Здатись";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(61, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // playerSide
            // 
            this.playerSide.BackColor = System.Drawing.Color.Transparent;
            this.playerSide.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.playerSide.Location = new System.Drawing.Point(401, 269);
            this.playerSide.Name = "playerSide";
            this.playerSide.Size = new System.Drawing.Size(140, 37);
            this.playerSide.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(61, 403);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Metadata:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(401, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ваша сторона:";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(401, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 26);
            this.label4.TabIndex = 6;
            this.label4.Text = "Таймер:";
            // 
            // timeForTurnLeftLabel
            // 
            this.timeForTurnLeftLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeForTurnLeftLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.timeForTurnLeftLabel.Location = new System.Drawing.Point(524, 33);
            this.timeForTurnLeftLabel.Name = "timeForTurnLeftLabel";
            this.timeForTurnLeftLabel.Size = new System.Drawing.Size(85, 26);
            this.timeForTurnLeftLabel.TabIndex = 7;
            this.timeForTurnLeftLabel.Text = "30";
            // 
            // winScreenLabel
            // 
            this.winScreenLabel.BackColor = System.Drawing.Color.Transparent;
            this.winScreenLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.winScreenLabel.Location = new System.Drawing.Point(61, 33);
            this.winScreenLabel.Name = "winScreenLabel";
            this.winScreenLabel.Size = new System.Drawing.Size(320, 26);
            this.winScreenLabel.TabIndex = 8;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(794, 571);
            this.Controls.Add(this.winScreenLabel);
            this.Controls.Add(this.timeForTurnLeftLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.playerSide);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnLeavingLobby);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.Shown += new System.EventHandler(this.GameForm_Shown);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label timeForTurnLeftLabel;
        private System.Windows.Forms.Label winScreenLabel;

        private System.Windows.Forms.Label playerSide;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.Panel panel1;

        #endregion
    }
}