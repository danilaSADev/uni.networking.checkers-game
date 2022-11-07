using System.ComponentModel;

namespace CheckersClient
{
    partial class GamesListForm
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
            this.roomsList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.leaderboard = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tournamentBox = new System.Windows.Forms.CheckBox();
            this.createLobbyButton = new System.Windows.Forms.Button();
            this.connectToGameButton = new System.Windows.Forms.Button();
            this.refreshLobbyButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.roomsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leaderboard)).BeginInit();
            this.SuspendLayout();
            // 
            // roomsList
            // 
            this.roomsList.BackgroundColor = System.Drawing.Color.DimGray;
            this.roomsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.roomsList.Location = new System.Drawing.Point(240, 53);
            this.roomsList.Name = "roomsList";
            this.roomsList.Size = new System.Drawing.Size(325, 362);
            this.roomsList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(240, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Доступні лоббі";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 36);
            this.label2.TabIndex = 3;
            this.label2.Text = "Список гравців";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // leaderboard
            // 
            this.leaderboard.BackgroundColor = System.Drawing.Color.DimGray;
            this.leaderboard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leaderboard.Location = new System.Drawing.Point(12, 53);
            this.leaderboard.Name = "leaderboard";
            this.leaderboard.Size = new System.Drawing.Size(222, 362);
            this.leaderboard.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(571, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 36);
            this.label3.TabIndex = 4;
            this.label3.Text = "Налаштування лоббі";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioButton1
            // 
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.radioButton1.Location = new System.Drawing.Point(571, 149);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(108, 30);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Стандартна гра";
            this.radioButton1.UseVisualStyleBackColor = false;
            // 
            // radioButton2
            // 
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.radioButton2.Location = new System.Drawing.Point(571, 174);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(108, 30);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Ускладнена гра";
            this.radioButton2.UseVisualStyleBackColor = false;
            // 
            // tournamentBox
            // 
            this.tournamentBox.BackColor = System.Drawing.Color.Transparent;
            this.tournamentBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tournamentBox.Location = new System.Drawing.Point(571, 221);
            this.tournamentBox.Name = "tournamentBox";
            this.tournamentBox.Size = new System.Drawing.Size(108, 22);
            this.tournamentBox.TabIndex = 7;
            this.tournamentBox.Text = "Турнір";
            this.tournamentBox.UseVisualStyleBackColor = false;
            // 
            // createLobbyButton
            // 
            this.createLobbyButton.BackColor = System.Drawing.Color.Transparent;
            this.createLobbyButton.BackgroundImage = global::CheckersClient.Properties.Resources.button_bg;
            this.createLobbyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.createLobbyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createLobbyButton.Location = new System.Drawing.Point(604, 249);
            this.createLobbyButton.Name = "createLobbyButton";
            this.createLobbyButton.Size = new System.Drawing.Size(146, 28);
            this.createLobbyButton.TabIndex = 8;
            this.createLobbyButton.Text = "Створити лоббі";
            this.createLobbyButton.UseVisualStyleBackColor = false;
            this.createLobbyButton.Click += new System.EventHandler(this.OnCreateLobby);
            // 
            // connectToGameButton
            // 
            this.connectToGameButton.BackColor = System.Drawing.Color.Transparent;
            this.connectToGameButton.BackgroundImage = global::CheckersClient.Properties.Resources.button_bg;
            this.connectToGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.connectToGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectToGameButton.Location = new System.Drawing.Point(593, 47);
            this.connectToGameButton.Name = "connectToGameButton";
            this.connectToGameButton.Size = new System.Drawing.Size(167, 28);
            this.connectToGameButton.TabIndex = 9;
            this.connectToGameButton.Text = "Приєднатись до гри";
            this.connectToGameButton.UseVisualStyleBackColor = false;
            // 
            // refreshLobbyButton
            // 
            this.refreshLobbyButton.BackColor = System.Drawing.Color.Transparent;
            this.refreshLobbyButton.BackgroundImage = global::CheckersClient.Properties.Resources.button_bg;
            this.refreshLobbyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.refreshLobbyButton.Font = new System.Drawing.Font("Matura MT Script Capitals", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshLobbyButton.Location = new System.Drawing.Point(517, 10);
            this.refreshLobbyButton.Name = "refreshLobbyButton";
            this.refreshLobbyButton.Size = new System.Drawing.Size(48, 41);
            this.refreshLobbyButton.TabIndex = 10;
            this.refreshLobbyButton.Text = "⟲";
            this.refreshLobbyButton.UseVisualStyleBackColor = false;
            this.refreshLobbyButton.Click += new System.EventHandler(this.OnRefreshLobbies);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::CheckersClient.Properties.Resources.button_bg;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Matura MT Script Capitals", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(186, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 41);
            this.button2.TabIndex = 12;
            this.button2.Text = "⟲";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.OnRefreshLeaderboard);
            // 
            // GamesListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CheckersClient.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.refreshLobbyButton);
            this.Controls.Add(this.connectToGameButton);
            this.Controls.Add(this.createLobbyButton);
            this.Controls.Add(this.tournamentBox);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.leaderboard);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.roomsList);
            this.Name = "GamesListForm";
            this.Text = "GamesForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnFormLoaded);
            ((System.ComponentModel.ISupportInitialize)(this.roomsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leaderboard)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button refreshLobbyButton;

        private System.Windows.Forms.Button button3;

        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.Button connectToGameButton;

        private System.Windows.Forms.Button createLobbyButton;

        private System.Windows.Forms.CheckBox tournamentBox;

        private System.Windows.Forms.RadioButton radioButton2;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton1;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView leaderboard;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.DataGridView roomsList;

        #endregion
    }
}