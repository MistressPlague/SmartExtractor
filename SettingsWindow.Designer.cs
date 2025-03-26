namespace SmartExtractor
{
    partial class SettingsWindow
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
            this.AutomationsGridView = new System.Windows.Forms.DataGridView();
            this.DestinationLabel = new System.Windows.Forms.Label();
            this.DestinationTextBox = new System.Windows.Forms.TextBox();
            this.DeleteEntryButton = new System.Windows.Forms.Button();
            this.ChooseDefaultDestinationButton = new System.Windows.Forms.Button();
            this.AutoExtract = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Regex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Choose = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.AutomationsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AutomationsGridView
            // 
            this.AutomationsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AutomationsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AutoExtract,
            this.Regex,
            this.Destination,
            this.Choose});
            this.AutomationsGridView.Location = new System.Drawing.Point(12, 41);
            this.AutomationsGridView.Name = "AutomationsGridView";
            this.AutomationsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AutomationsGridView.Size = new System.Drawing.Size(557, 154);
            this.AutomationsGridView.TabIndex = 0;
            this.AutomationsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AutomationsGridView_CellContentClick);
            this.AutomationsGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.AutomationsGridView_CellValueChanged);
            this.AutomationsGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.AutomationsGridView_RowsRemoved);
            // 
            // DestinationLabel
            // 
            this.DestinationLabel.AutoSize = true;
            this.DestinationLabel.Location = new System.Drawing.Point(10, 215);
            this.DestinationLabel.Name = "DestinationLabel";
            this.DestinationLabel.Size = new System.Drawing.Size(100, 13);
            this.DestinationLabel.TabIndex = 1;
            this.DestinationLabel.Text = "Default Destination:";
            // 
            // DestinationTextBox
            // 
            this.DestinationTextBox.Location = new System.Drawing.Point(118, 212);
            this.DestinationTextBox.Name = "DestinationTextBox";
            this.DestinationTextBox.Size = new System.Drawing.Size(370, 20);
            this.DestinationTextBox.TabIndex = 2;
            // 
            // DeleteEntryButton
            // 
            this.DeleteEntryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteEntryButton.Location = new System.Drawing.Point(546, 12);
            this.DeleteEntryButton.Name = "DeleteEntryButton";
            this.DeleteEntryButton.Size = new System.Drawing.Size(23, 23);
            this.DeleteEntryButton.TabIndex = 3;
            this.DeleteEntryButton.Text = "X";
            this.DeleteEntryButton.UseVisualStyleBackColor = true;
            this.DeleteEntryButton.Click += new System.EventHandler(this.DeleteEntryButton_Click);
            // 
            // ChooseDefaultDestinationButton
            // 
            this.ChooseDefaultDestinationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChooseDefaultDestinationButton.Location = new System.Drawing.Point(494, 210);
            this.ChooseDefaultDestinationButton.Name = "ChooseDefaultDestinationButton";
            this.ChooseDefaultDestinationButton.Size = new System.Drawing.Size(75, 23);
            this.ChooseDefaultDestinationButton.TabIndex = 4;
            this.ChooseDefaultDestinationButton.Text = "Choose";
            this.ChooseDefaultDestinationButton.UseVisualStyleBackColor = true;
            this.ChooseDefaultDestinationButton.Click += new System.EventHandler(this.ChooseDefaultDestinationButton_Click);
            // 
            // AutoExtract
            // 
            this.AutoExtract.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AutoExtract.FillWeight = 50F;
            this.AutoExtract.HeaderText = "Auto Extract";
            this.AutoExtract.Name = "AutoExtract";
            // 
            // Regex
            // 
            this.Regex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Regex.FillWeight = 50F;
            this.Regex.HeaderText = "Regex";
            this.Regex.Name = "Regex";
            // 
            // Destination
            // 
            this.Destination.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Destination.FillWeight = 50F;
            this.Destination.HeaderText = "Destination";
            this.Destination.Name = "Destination";
            // 
            // Choose
            // 
            this.Choose.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Choose.FillWeight = 50F;
            this.Choose.HeaderText = "Choose";
            this.Choose.Name = "Choose";
            this.Choose.Text = "Choose";
            this.Choose.ToolTipText = "Choose Destination";
            this.Choose.UseColumnTextForButtonValue = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 245);
            this.Controls.Add(this.ChooseDefaultDestinationButton);
            this.Controls.Add(this.DeleteEntryButton);
            this.Controls.Add(this.DestinationTextBox);
            this.Controls.Add(this.DestinationLabel);
            this.Controls.Add(this.AutomationsGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Automations And Settings";
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.Shown += new System.EventHandler(this.SettingsWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.AutomationsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AutomationsGridView;
        private System.Windows.Forms.Label DestinationLabel;
        private System.Windows.Forms.TextBox DestinationTextBox;
        private System.Windows.Forms.Button DeleteEntryButton;
        private System.Windows.Forms.Button ChooseDefaultDestinationButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AutoExtract;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destination;
        private System.Windows.Forms.DataGridViewButtonColumn Choose;
    }
}