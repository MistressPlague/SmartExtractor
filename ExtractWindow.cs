using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Libraries;
using SharpCompress.Archives;
using SharpCompress.Archives.GZip;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Tar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;
using SmartExtractor.Special_Types;
using SmartExtractor.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmartExtractor
{
    public partial class ExtractWindow : Form
    {
        public class Configuration
        {
            public List<Automation> Automations = new List<Automation>();
            public string DefaultLocation;
        }

        public class Automation
        {
            public bool AutoExtract;
            public string ApplyToThisRegex;
            public string Destination;
        }

        public static ConfigLib<Configuration> Config;

        private readonly string ArchivePath;

        public static string TempPath;

        public ExtractWindow(string[] args)
        {
            InitializeComponent();

            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            TempPath = Environment.CurrentDirectory + "\\Temp";

            Directory.CreateDirectory(TempPath);

            Config = new ConfigLib<Configuration>(Environment.CurrentDirectory + "\\Config.json");

            if (args.Length > 0)
            {
                ArchivePath = args[0]; // Assumes first argument is the file path
            }
        }

        private void ExtractWindow_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Config.InternalConfig.DefaultLocation))
            {
                Config.InternalConfig.DefaultLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            DestinationTextBox.Text = Config.InternalConfig.DefaultLocation;

            if (!string.IsNullOrEmpty(ArchivePath))
            {
                if (!File.Exists(ArchivePath))
                {
                    MessageBox.Show("Supplied Archive Path Argument Is Not A Valid File!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                    return; // Just in case of harmony for some reason
                }

                Text += $": {Path.GetFileName(ArchivePath)}";

                DestinationLabel.Enabled = true;
                DestinationTextBox.Enabled = true;
                ChooseButton.Enabled = true;
                SetDefaultButton.Enabled = false;
                ExtractButton.Enabled = true;

                // Setup
                if (Config.InternalConfig.Automations.FirstOrDefault(o => !string.IsNullOrEmpty(o.ApplyToThisRegex) && !string.IsNullOrEmpty(o.Destination) && Regex.IsMatch(Path.GetFileName(ArchivePath), o.ApplyToThisRegex)) is var match && match != null)
                {
                    DestinationTextBox.Text = match.Destination;
                }

                if (match != null && match.AutoExtract)
                {
                    ExtractButton.PerformClick();
                }
            }

            HasInit = true;
        }

        private IArchive ReadArchive(string path)
        {
            IArchive result = null;

            using (Stream stream = File.OpenRead(path))
            using (var reader = ReaderFactory.Open(stream))
            {
                switch (reader.ArchiveType)
                {
                    case ArchiveType.Rar:
                        result = RarArchive.Open(path);
                        break;
                    case ArchiveType.Zip:
                        result = ZipArchive.Open(path);
                        break;
                    case ArchiveType.Tar:
                        switch (Path.GetExtension(path).ToLower())
                        {
                            case ".unitypackage":
                                result = ArchiveFactory.Open(path);
                                break;
                        }

                        if (result != null)
                        {
                            break;
                        }

                        result = TarArchive.Open(path);
                        break;
                    case ArchiveType.SevenZip:
                        result = SevenZipArchive.Open(path);
                        break;
                    case ArchiveType.GZip:
                        result = GZipArchive.Open(path);
                        break;
                    default:
                        
                        break;
                }
            }

            return result;
        }

        private bool HasInit;
        private (List<(string, IArchiveEntry)>, (FileStream, string)?)? ProcessDestination(bool adjust = true)
        {
            (List<(string, IArchiveEntry)>, (FileStream, string)?)? data = null;
            
            DisplayButton.Enabled = false;
            
            if (Directory.Exists(DestinationTextBox.Text))
            {
                if (adjust)
                {
                    DestinationTextBox.Text += $"\\{Path.GetFileNameWithoutExtension(ArchivePath)}";
                }
                
                var HasAnyDifferentFiles = false;

                var archive = ReadArchive(ArchivePath);
                data = archive.GetEntries(ArchivePath);
                var entries = data.Value.Item1;

                if (entries.Count == 0)
                {
                    MessageBox.Show("The Archive Has No Contents! Closing..", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    Environment.Exit(0);
                }

                if (Directory.Exists(DestinationTextBox.Text))
                {
                    foreach (var entry in entries)
                    {
                        var file = entry.Item2;

                        if (!file.IsDirectory)
                        {
                            if (File.Exists(DestinationTextBox.Text + $"\\{entry.Item1}") && file.GetCRC() != Utilities.Utilities.CalculateCRC32(DestinationTextBox.Text + $"\\{entry.Item1}"))
                            {
                                HasAnyDifferentFiles = true;
                            }
                            else
                            {
                                HasAnyDifferentFiles = false;
                            }
                        }
                    }
                }
                else
                {
                    HasAnyDifferentFiles = true; // If destination doesn't exist, then it's different.
                }

                if (!HasAnyDifferentFiles)
                {
                    MessageBox.Show("The Destination Has The Exact Same Files As In This Archive! Opening Folder And Closing..", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    Process.Start("explorer.exe", DestinationTextBox.Text);

                    Environment.Exit(0);
                }

                DisplayButton.Enabled = true;
            }
            
            return data;
        }

        private void SetDefaultButton_Click(object sender, EventArgs e) // To Do: Add All Supported Exts
        {
            Utilities.Utilities.SetFileTypeAssociationCurrentUser("zip", "ZIP File", Application.ExecutablePath, Process.GetCurrentProcess().ProcessName + ".exe");

            Utilities.Utilities.SetFileTypeAssociationCurrentUser("rar", "RAR File", Application.ExecutablePath, Process.GetCurrentProcess().ProcessName + ".exe");

            Utilities.Utilities.SetFileTypeAssociationCurrentUser("7z", "7Z File", Application.ExecutablePath, Process.GetCurrentProcess().ProcessName + ".exe");

            Utilities.Utilities.SetFileTypeAssociationCurrentUser("gz", "GZ File", Application.ExecutablePath, Process.GetCurrentProcess().ProcessName + ".exe");

            Utilities.Utilities.SetFileTypeAssociationCurrentUser("tar", "TAR File", Application.ExecutablePath, Process.GetCurrentProcess().ProcessName + ".exe");

            Utilities.Utilities.SetFileTypeAssociationCurrentUser("unitypackage", "UnityPackage File", Application.ExecutablePath, Process.GetCurrentProcess().ProcessName + ".exe");

            MessageBox.Show("Done! Closing..", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Environment.Exit(0);
        }

        private void ExtractButton_Click(object sender, EventArgs e)
        {
            Enabled = false;

            Task.Run(() =>
            {
                var archivedata = ProcessDestination(!DestinationTextBox.Text.Contains(Path.GetFileNameWithoutExtension(ArchivePath)));

                if (archivedata == null)
                {
                    Environment.Exit(0);
                }

                var data = archivedata.Value;

                if (!Directory.Exists(DestinationTextBox.Text))
                {
                    Directory.CreateDirectory(DestinationTextBox.Text);
                }

                var entries = data.Item1;

                progressBar1.Minimum = 0;
                progressBar1.Maximum = entries.Count;

                //Application.DoEvents();

                for (var index = 0; index < entries.Count; index++)
                {
                    var file = entries[index].Item2;

                    progressBar1.Value = index;
                    //Application.DoEvents();

                    file.WriteToDirectory(DestinationTextBox.Text, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true }, entries[index].Item1);
                }

                progressBar1.Value = entries.Count;

                //Application.DoEvents();

                Process.Start("explorer.exe", DestinationTextBox.Text);

                if (data.Item2 != null)
                {
                    data.Item2.Value.Item1.Close();
                    data.Item2.Value.Item1.Dispose();
                    File.Delete(data.Item2.Value.Item2);
                }

                Environment.Exit(0);
            });
        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            if (!HasInit)
            {
                return;
            }
            
            using var popup = new FolderBrowserDialog();

            if (popup.ShowDialog() == DialogResult.OK)
            {
                DestinationTextBox.Text = popup.SelectedPath;
            }
        }

        private void DisplayButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", DestinationTextBox.Text);
        }

        private void AutomationsAndSettingsButton_Click(object sender, EventArgs e)
        {
            using var settings = new SettingsWindow();

            settings.ShowDialog();
        }

        private void ExtractWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Directory.Delete(TempPath, true);
        }
    }
}