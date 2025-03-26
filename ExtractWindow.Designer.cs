namespace SmartExtractor
{
    partial class ExtractWindow
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
            this.DestinationLabel = new System.Windows.Forms.Label();
            this.DestinationTextBox = new System.Windows.Forms.TextBox();
            this.DisplayButton = new System.Windows.Forms.Button();
            this.ChooseButton = new System.Windows.Forms.Button();
            this.SetDefaultButton = new System.Windows.Forms.Button();
            this.AutomationsAndSettingsButton = new System.Windows.Forms.Button();
            this.ExtractButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // DestinationLabel
            // 
            this.DestinationLabel.AutoSize = true;
            this.DestinationLabel.Enabled = false;
            this.DestinationLabel.Location = new System.Drawing.Point(13, 11);
            this.DestinationLabel.Name = "DestinationLabel";
            this.DestinationLabel.Size = new System.Drawing.Size(63, 13);
            this.DestinationLabel.TabIndex = 0;
            this.DestinationLabel.Text = "Destination:";
            // 
            // DestinationTextBox
            // 
            this.DestinationTextBox.Enabled = false;
            this.DestinationTextBox.Location = new System.Drawing.Point(81, 8);
            this.DestinationTextBox.Name = "DestinationTextBox";
            this.DestinationTextBox.Size = new System.Drawing.Size(231, 20);
            this.DestinationTextBox.TabIndex = 1;
            // 
            // DisplayButton
            // 
            this.DisplayButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DisplayButton.Enabled = false;
            this.DisplayButton.Location = new System.Drawing.Point(399, 7);
            this.DisplayButton.Name = "DisplayButton";
            this.DisplayButton.Size = new System.Drawing.Size(75, 23);
            this.DisplayButton.TabIndex = 2;
            this.DisplayButton.Text = "Display Folder";
            this.DisplayButton.UseVisualStyleBackColor = true;
            this.DisplayButton.Click += new System.EventHandler(this.DisplayButton_Click);
            // 
            // ChooseButton
            // 
            this.ChooseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChooseButton.Enabled = false;
            this.ChooseButton.Location = new System.Drawing.Point(318, 7);
            this.ChooseButton.Name = "ChooseButton";
            this.ChooseButton.Size = new System.Drawing.Size(75, 23);
            this.ChooseButton.TabIndex = 3;
            this.ChooseButton.Text = "Choose";
            this.ChooseButton.UseVisualStyleBackColor = true;
            this.ChooseButton.Click += new System.EventHandler(this.ChooseButton_Click);
            // 
            // SetDefaultButton
            // 
            this.SetDefaultButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SetDefaultButton.Location = new System.Drawing.Point(399, 121);
            this.SetDefaultButton.Name = "SetDefaultButton";
            this.SetDefaultButton.Size = new System.Drawing.Size(75, 23);
            this.SetDefaultButton.TabIndex = 4;
            this.SetDefaultButton.Text = "Set Default";
            this.SetDefaultButton.UseVisualStyleBackColor = true;
            this.SetDefaultButton.Click += new System.EventHandler(this.SetDefaultButton_Click);
            // 
            // AutomationsAndSettingsButton
            // 
            this.AutomationsAndSettingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AutomationsAndSettingsButton.Location = new System.Drawing.Point(15, 34);
            this.AutomationsAndSettingsButton.Name = "AutomationsAndSettingsButton";
            this.AutomationsAndSettingsButton.Size = new System.Drawing.Size(459, 23);
            this.AutomationsAndSettingsButton.TabIndex = 5;
            this.AutomationsAndSettingsButton.Text = "Automations And Settings";
            this.AutomationsAndSettingsButton.UseVisualStyleBackColor = true;
            this.AutomationsAndSettingsButton.Click += new System.EventHandler(this.AutomationsAndSettingsButton_Click);
            // 
            // ExtractButton
            // 
            this.ExtractButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExtractButton.Enabled = false;
            this.ExtractButton.Location = new System.Drawing.Point(15, 63);
            this.ExtractButton.Name = "ExtractButton";
            this.ExtractButton.Size = new System.Drawing.Size(459, 23);
            this.ExtractButton.TabIndex = 6;
            this.ExtractButton.Text = "Extract";
            this.ExtractButton.UseVisualStyleBackColor = true;
            this.ExtractButton.Click += new System.EventHandler(this.ExtractButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 92);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(459, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // ExtractWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 152);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ExtractButton);
            this.Controls.Add(this.AutomationsAndSettingsButton);
            this.Controls.Add(this.SetDefaultButton);
            this.Controls.Add(this.ChooseButton);
            this.Controls.Add(this.DisplayButton);
            this.Controls.Add(this.DestinationTextBox);
            this.Controls.Add(this.DestinationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ExtractWindow";
            this.ShowIcon = false;
            this.Text = "SmartExtractor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExtractWindow_FormClosed);
            this.Load += new System.EventHandler(this.ExtractWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DestinationLabel;
        private System.Windows.Forms.TextBox DestinationTextBox;
        private System.Windows.Forms.Button DisplayButton;
        private System.Windows.Forms.Button ChooseButton;
        private System.Windows.Forms.Button SetDefaultButton;
        private System.Windows.Forms.Button AutomationsAndSettingsButton;
        private System.Windows.Forms.Button ExtractButton;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

