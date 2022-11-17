namespace CheckersClient
{
    partial class IndexForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndexForm));
            this.authorizeButton = new System.Windows.Forms.Button();
            this.usernameBox = new System.Windows.Forms.RichTextBox();
            this.passwordBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // authorizeButton
            // 
            this.authorizeButton.BackgroundImage = global::CheckersClient.Properties.Resources.button_bg;
            this.authorizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.authorizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.authorizeButton.Location = new System.Drawing.Point(318, 241);
            this.authorizeButton.Name = "authorizeButton";
            this.authorizeButton.Size = new System.Drawing.Size(146, 28);
            this.authorizeButton.TabIndex = 0;
            this.authorizeButton.Text = "Увійти в гру";
            this.authorizeButton.UseVisualStyleBackColor = true;
            this.authorizeButton.Click += new System.EventHandler(this.OnAuthorize);
            // 
            // usernameBox
            // 
            this.usernameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usernameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.usernameBox.Location = new System.Drawing.Point(277, 127);
            this.usernameBox.MaxLength = 50;
            this.usernameBox.Multiline = false;
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(229, 33);
            this.usernameBox.TabIndex = 1;
            this.usernameBox.Text = "";
            // 
            // passwordBox
            // 
            this.passwordBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passwordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordBox.Location = new System.Drawing.Point(277, 191);
            this.passwordBox.MaxLength = 50;
            this.passwordBox.Multiline = false;
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(229, 33);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.Text = "";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(277, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ваш нікнейм";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(277, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ваш пароль";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // IndexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CheckersClient.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.authorizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IndexForm";
            this.Text = "Authorization ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.RichTextBox passwordBox;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.RichTextBox usernameBox;

        private System.Windows.Forms.Button authorizeButton;

        #endregion
    }
}