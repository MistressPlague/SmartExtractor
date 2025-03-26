using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartExtractor
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void SettingsWindow_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ExtractWindow.Config.InternalConfig.DefaultLocation))
            {
                DestinationTextBox.Text = ExtractWindow.Config.InternalConfig.DefaultLocation;
            }

            foreach (var automation in ExtractWindow.Config.InternalConfig.Automations)
            {
                AutomationsGridView.Rows.Add(automation.AutoExtract, automation.ApplyToThisRegex, automation.Destination, "Choose");
            }
        }

        private void AutomationsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;

            if (index == -1)
            {
                return;
            }

            ExtractWindow.Automation entry;

            if (index >= ExtractWindow.Config.InternalConfig.Automations.Count) // Within Range
            {
                entry = new ExtractWindow.Automation();

                ExtractWindow.Config.InternalConfig.Automations.Add(entry);
            }
            else
            {
                entry = ExtractWindow.Config.InternalConfig.Automations[index];
            }

            switch (e.ColumnIndex)
            {
                case 0: // Auto Extract
                    entry.AutoExtract = (bool)AutomationsGridView.Rows[index].Cells[e.ColumnIndex].EditedFormattedValue;
                    Text = $"Auto Extract Changed: {entry.AutoExtract}";
                    break;
                case 1: // Regex
                    entry.ApplyToThisRegex = AutomationsGridView.Rows[index].Cells[e.ColumnIndex].Value.ToString();
                    Text = $"Regex Changed: {entry.ApplyToThisRegex}";
                    break;
                case 2: // Destination
                    entry.Destination = AutomationsGridView.Rows[index].Cells[e.ColumnIndex].Value.ToString();
                    Text = $"Destination Changed: {entry.Destination}";
                    break;
                case 3: // Button - how the fuck does value change?
                {
                    using var popup = new FolderBrowserDialog();

                    if (popup.ShowDialog() == DialogResult.OK)
                    {
                        AutomationsGridView.Rows[e.RowIndex].Cells[2].Value = popup.SelectedPath;
                    }
                    break;
                }
            }

            ExtractWindow.Config.InternalConfig.Automations[index] = entry;
        }

        private void AutomationsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                if (((DataGridView)sender).Columns[e.ColumnIndex] is not DataGridViewTextBoxColumn)
                {
                    AutomationsGridView_CellValueChanged(sender, e);
                }
            }
        }

        private void DeleteEntryButton_Click(object sender, EventArgs e)
        {
            if (AutomationsGridView.SelectedRows.Count > 0 && !AutomationsGridView.SelectedRows[0].IsNewRow)
            {
                AutomationsGridView.Rows.Remove(AutomationsGridView.SelectedRows[0]);
            }
        }

        private void AutomationsGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            var index = e.RowIndex;

            if (index == -1)
            {
                return;
            }

            if (index < ExtractWindow.Config.InternalConfig.Automations.Count) // Within Range
            {
                ExtractWindow.Config.InternalConfig.Automations.RemoveAt(index);
            }
        }

        private void ChooseDefaultDestinationButton_Click(object sender, EventArgs e)
        {
            using var popup = new FolderBrowserDialog();

            if (popup.ShowDialog() == DialogResult.OK)
            {
                DestinationTextBox.Text = popup.SelectedPath;
                ExtractWindow.Config.InternalConfig.DefaultLocation = popup.SelectedPath;
            }
        }
    }
}